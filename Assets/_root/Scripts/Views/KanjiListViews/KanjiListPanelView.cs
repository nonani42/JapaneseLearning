using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class KanjiListPanelView : PanelView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _kanjiButtonHolder;
        [SerializeField] private KanjiButtonView _kanjiButtonPrefab;

        [Header("Statistics")]
        [SerializeField] private Color _allStatColor;
        [SerializeField] private Transform _statHolder;
        [SerializeField] private StatTextView _statTextPrefab;

        private StatsModel<KanjiCardSO> _statModel;
        private List<KanjiButtonView> _activeKanjiButtonViews = new();
        private List<KanjiButtonView> _inactiveKanjiButtonViews = new();
        private Dictionary<KanjiCardSO, bool> _kanjiDictionary = new();

        public event Action OnBack;
        public event Action<char, bool> OnSelectionChanged;

        private void Start() => _backButton.onClick.AddListener(() => OnBack?.Invoke());

        public void Init(Dictionary<KanjiCardSO, bool> kanjiDictionary)
        {
            _kanjiDictionary = kanjiDictionary;
            _statModel = new(_statHolder, _statTextPrefab, _allStatColor);

            ClearScreen();
            _statModel.ClearStatScreen();

            foreach (var kanji in kanjiDictionary)
            {
                KanjiButtonView view;
                if (_inactiveKanjiButtonViews.Count == 0)
                    view = Instantiate(_kanjiButtonPrefab.gameObject, _kanjiButtonHolder).GetComponent<KanjiButtonView>();
                else
                    view = GetFromPool();

                view.SetButton(kanji.Key.Kanji, kanji.Value);
                view.OnSelectionChanged += ChangeSelection;
                view.TurnOn(true);

                _activeKanjiButtonViews.Add(view);
            }

            _statModel.DoStats(kanjiDictionary);
        }

        public void ChangeSelection(char kanji, bool isKnown)
        {
            KanjiCardSO card = _kanjiDictionary.Keys
                .Where(t => t.Kanji == kanji)
                .Select(k => k)
                .First();

            _kanjiDictionary[card] = isKnown;

            _statModel.DoStats(_kanjiDictionary);
            OnSelectionChanged?.Invoke(kanji, isKnown);
        }

        private void ClearScreen()
        {
            if (_activeKanjiButtonViews.Count >= 0)
            {
                for (int i = _activeKanjiButtonViews.Count - 1; i >= 0; i--)
                {
                    ReturnToPool(_activeKanjiButtonViews[i]);
                }
            }
        }

        private KanjiButtonView GetFromPool()
        {
            KanjiButtonView result = _inactiveKanjiButtonViews.Last();
            _inactiveKanjiButtonViews.Remove(result);
            return result;
        }

        private void ReturnToPool(KanjiButtonView view)
        {
            view.TurnOn(false);
            _inactiveKanjiButtonViews.Add(view);
            _activeKanjiButtonViews.Remove(view);
        }


        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();

            foreach (var view in _activeKanjiButtonViews)
            {
                view.OnSelectionChanged -= ChangeSelection;
            }

            foreach (var view in _inactiveKanjiButtonViews)
            {
                view.OnSelectionChanged -= ChangeSelection;
            }

            _activeKanjiButtonViews.Clear();
            _inactiveKanjiButtonViews.Clear();
        }
    }
}
