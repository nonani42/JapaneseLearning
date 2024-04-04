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

        private List<KanjiButtonView> _activeKanjiButtonViews = new List<KanjiButtonView>();
        private List<KanjiButtonView> _inactiveKanjiButtonViews = new List<KanjiButtonView>();

        public event Action OnBack;
        public event Action<char, bool> OnSelectionChanged;

        private void Start() => _backButton.onClick.AddListener(() => OnBack?.Invoke());

        public void Init(Dictionary<char, bool> kanjiDictionary)
        {
            ClearScreen();

            foreach (var kanji in kanjiDictionary)
            {
                KanjiButtonView view;
                if (_inactiveKanjiButtonViews.Count == 0)
                    view = Instantiate(_kanjiButtonPrefab.gameObject, _kanjiButtonHolder).GetComponent<KanjiButtonView>();
                else
                    view = GetFromPool();

                view.SetButton(kanji.Key, kanji.Value);
                view.OnSelectionChanged += ChangeSelection;
                view.TurnOn(true);

                _activeKanjiButtonViews.Add(view);
            }
        }

        public void ChangeSelection(char kanji, bool isKnown) => OnSelectionChanged?.Invoke(kanji, isKnown);

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
