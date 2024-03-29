using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestSpace
{
    internal class KanjiListController : IController
    {
        private KanjiListPanelView _kanjiListPanelView;
        private KanjiSO[] _kanjiArr;
        private List<string> _knownKanjiList = new List<string>();
        private Dictionary<string, bool> _kanjiDictionary = new Dictionary<string, bool>();
        private LoadSaveModel _loadSaveModel = new LoadSaveModel();

        public event Action<List<string>> OnKnownKanjiListUpdate;

        public KanjiListController(KanjiListPanelView kanjiListPanelView, KanjiSO[] kanjiArr)
        {
            _kanjiListPanelView = kanjiListPanelView;
            _kanjiArr = kanjiArr;
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
            _knownKanjiList = _loadSaveModel.LoadKnownKanji();
            _kanjiDictionary.Clear();

            for (int i = 0; i < _kanjiArr.Length; i++)
            {
                bool isKnown = false;

                if(_knownKanjiList.Contains(_kanjiArr[i].Kanji))
                    isKnown = true;

                _kanjiDictionary.Add(_kanjiArr[i].Kanji, isKnown);
            }
        }

        private void ChangeKnownKanji(string kanji, bool isKnown)
        {
            if (_knownKanjiList.Contains(kanji) && !isKnown)
                _knownKanjiList.Remove(kanji);

            if (!_knownKanjiList.Contains(kanji) && isKnown)
                _knownKanjiList.Add(kanji);
        }

        private void GoBackToMenu()
        {
            _loadSaveModel.SaveKnownKanji(_knownKanjiList);
            OnKnownKanjiListUpdate?.Invoke(_knownKanjiList);
        }

        public void Destroy()
        {
            _kanjiListPanelView.OnSelectionChanged -= ChangeKnownKanji;
            _kanjiListPanelView.OnBack -= GoBackToMenu;
        }
    }
}
