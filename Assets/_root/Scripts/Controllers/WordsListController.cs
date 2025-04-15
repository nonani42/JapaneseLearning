using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSpace
{
    internal class WordsListController : IController
    {
        private WordsListPanelView _wordsListPanelView;
        private WordSO[] _allWordsArr;
        private KanaSO[] _allKanaArr;
        private List<string> _knownWordsList = new();
        private List<char> _knownKanjiList = new();
        private Dictionary<WordSO, bool> _wordsDictionary = new();

        private char[] _symbols = new[]
        { '~', '〜', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(', ')', ' ' };

        public event Action<List<string>> OnKnownWordsListUpdate;

        public WordsListController(WordsListPanelView wordsListPanelView, WordSO[] allWordsArr, List<string> knownWordsList, List<char> knownKanjiList, KanaSO[] allKanaArr)
        {
            _wordsListPanelView = wordsListPanelView;
            _allWordsArr = allWordsArr;
            _allKanaArr = allKanaArr;
            _knownWordsList = knownWordsList;
            _knownKanjiList = knownKanjiList;
            FillList(_knownWordsList.Count == 0);
        }

        public void Init()
        {
            FillViewList();

            _wordsListPanelView.Init(_wordsDictionary);
        }

        private void FillViewList()
        {
            _wordsDictionary.Clear();

            for (int i = 0; i < _allWordsArr.Length; i++)
            {
                bool isKnown = false;

                if (_knownWordsList.Contains(_allWordsArr[i].JpReading))
                    isKnown = true;

                _wordsDictionary.Add(_allWordsArr[i], isKnown);
            }
        }

        private void FillList(bool isEmpty)
        {
            if (!isEmpty)
                return;

            string[] words = _allWordsArr.Where(w => w.KanaOnly).Select(w => w.JpReading).ToArray();
            if (!_knownWordsList.Contains(words[0]))
                _knownWordsList.AddRange(words);

            for (int i = 0; i < _knownKanjiList.Count; i++)
                AddWords(_knownKanjiList[i]);
            OnKnownWordsListUpdate?.Invoke(_knownWordsList);
        }

        public void ChangeKnownWords(char kanji, bool isKnown)
        {
            if (isKnown)
            {
                AddWords(kanji);
            }
            else
            {
                string[] words = _knownWordsList.Where(w => w.Contains(kanji)).Select(w => w).ToArray();
                for (int i = 0; i < words.Length; i++)
                {
                    if (_knownWordsList.Contains(words[i]))
                        _knownWordsList.Remove(words[i]);
                }
            }
            OnKnownWordsListUpdate?.Invoke(_knownWordsList);
        }

        private void AddWords(char kanji)
        {
            string[] words = _allWordsArr.Where(w => w.JpReading.Contains(kanji)).Select(w => w.JpReading).ToArray();

            List<string> checkedWords = new();
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                word = word.Remove(word.IndexOf(kanji), 1);

                int index = 0;
                while (word.Length != 0 && index < _symbols.Length)
                {
                    if (word.Contains(_symbols[index]))
                        word = word.Remove(word.IndexOf(_symbols[index]), 1);
                    index++;
                }

                index = 0;
                while (word.Length != 0 && index < _allKanaArr.Length)
                {
                    int index2 = 0;
                    while (word.Length != 0 && index2 < _allKanaArr[index].HiraganaKeyName.Length)
                    {
                        if (word.Contains(_allKanaArr[index].HiraganaKeyName.ToCharArray()[index2]))
                            word = word.Remove(word.IndexOf(_allKanaArr[index].HiraganaKeyName.ToCharArray()[index2]), 1);
                        index2++;
                    }
                    index++;
                }

                index = 0;
                while (word.Length != 0 && index < _allKanaArr.Length)
                {
                    int index2 = 0;
                    while (word.Length != 0 && index2 < _allKanaArr[index].KatakanaKeyName.Length)
                    {
                        if (word.Contains(_allKanaArr[index].KatakanaKeyName.ToCharArray()[index2]))
                            word = word.Remove(word.IndexOf(_allKanaArr[index].KatakanaKeyName.ToCharArray()[index2]), 1);
                        index2++;
                    }
                    index++;
                }

                index = 0;
                while (word.Length != 0 && index < _knownKanjiList.Count)
                {
                    if (word.Contains(_knownKanjiList[index]))
                    {
                        word = word.Remove(word.IndexOf(_knownKanjiList[index]), 1);
                    }

                    index++;
                }

                if (word.Length == 0 && !_knownWordsList.Contains(words[i]))
                    _knownWordsList.Add(words[i]);
            }
        }

        public void KnownKanjiListChange(List<char> knownKanjiList) => _knownKanjiList = knownKanjiList;

        public void Destroy()
        {
        }
    }
}
