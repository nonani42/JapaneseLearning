using System;
using System.Collections.Generic;

namespace TestSpace
{
    internal class KanjiListController : IController
    {
        private KanjiListPanelView _kanjiListPanelView;
        private KanjiCardSO[] _kanjiArr;
        private List<char> _knownKanjiList = new();
        private Dictionary<char, bool> _kanjiDictionary = new();

        public event Action<List<char>> OnKnownKanjiListUpdate;

        public KanjiListController(KanjiListPanelView kanjiListPanelView, KanjiCardSO[] kanjiArr, List<char> knownKanjiList)
        {
            _kanjiListPanelView = kanjiListPanelView;
            _kanjiArr = kanjiArr;
            _knownKanjiList = knownKanjiList;
            _kanjiListPanelView.OnSelectionChanged += ChangeKnownKanji;
            _kanjiListPanelView.OnBack += GoBackToMenu;
        }

        public void Init()
        {
            FillKanjiList();

            _kanjiListPanelView.Init(_kanjiDictionary);
        }

        private void FillKanjiList()
        {
            _kanjiDictionary.Clear();

            for (int i = 0; i < _kanjiArr.Length; i++)
            {
                bool isKnown = false;

                if(_knownKanjiList.Contains(_kanjiArr[i].Kanji))
                    isKnown = true;

                _kanjiDictionary.Add(_kanjiArr[i].Kanji, isKnown);
            }
        }

        private void ChangeKnownKanji(char kanji, bool isKnown)
        {
            if (_knownKanjiList.Contains(kanji) && !isKnown)
                _knownKanjiList.Remove(kanji);

            if (!_knownKanjiList.Contains(kanji) && isKnown)
                _knownKanjiList.Add(kanji);
        }

        private void GoBackToMenu() => OnKnownKanjiListUpdate?.Invoke(_knownKanjiList);

        public void Destroy()
        {
            _kanjiListPanelView.OnSelectionChanged -= ChangeKnownKanji;
            _kanjiListPanelView.OnBack -= GoBackToMenu;
        }
    }
}
