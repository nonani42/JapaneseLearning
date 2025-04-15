using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class WordsListPanelView : PanelView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _wordHolder;
        [SerializeField] private WordView _wordViewPrefab;

        [Header("Statistics")]
        [SerializeField] private Color _allStatColor;
        [SerializeField] private Transform _statHolder;
        [SerializeField] private StatTextView _statTextPrefab;

        private StatsModel<WordSO> _statModel;
        private List<WordView> _activeWordsViews = new();
        private List<WordView> _inactiveWordsViews = new();

        public event Action OnBack;


        private void Start() => _backButton.onClick.AddListener(() => OnBack?.Invoke());

        public void Init(Dictionary<WordSO, bool> wordsDictionary)
        {
            _statModel = new(_statHolder, _statTextPrefab, _allStatColor);
            ClearScreen();
            _statModel.ClearStatScreen();

            StartCoroutine(CreateWords(wordsDictionary));

            _statModel.DoStats(wordsDictionary);
        }

        private void ClearScreen()
        {
            if (_activeWordsViews.Count >= 0)
            {
                for (int i = _activeWordsViews.Count - 1; i >= 0; i--)
                {
                    ReturnToPool(_activeWordsViews[i]);
                }
            }
        }

        private WordView GetFromPool()
        {
            WordView result = _inactiveWordsViews.Last();
            _inactiveWordsViews.Remove(result);
            return result;
        }

        private void ReturnToPool(WordView view)
        {
            view.TurnOn(false);
            _inactiveWordsViews.Add(view);
            _activeWordsViews.Remove(view);
        }


        private IEnumerator CreateWords(Dictionary<WordSO, bool> wordsDictionary)
        {
            int i = 0;
            foreach (KeyValuePair<WordSO, bool> word in wordsDictionary)
            {
                if (word.Value)
                {
                    i++;
                    WordView view;
                    if (_inactiveWordsViews.Count == 0)
                        view = Instantiate(_wordViewPrefab.gameObject, _wordHolder).GetComponent<WordView>();
                    else
                        view = GetFromPool();

                    view.SetWord(word.Key, word.Value);
                    view.TurnOn(true);

                    _activeWordsViews.Add(view);

                    if (i % 500 == 0)
                        yield return null;
                }
            }
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();

            _activeWordsViews.Clear();
            _inactiveWordsViews.Clear();
        }
    }
}
