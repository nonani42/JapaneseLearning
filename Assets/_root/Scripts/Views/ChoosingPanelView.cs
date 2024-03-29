using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    public class ChoosingPanelView : PanelView
    {
        [SerializeField] private TestTypePanelView _oralTypePanelView;
        [SerializeField] private TestTypePanelView _writingTypePanelView;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Button _kanjiListBtn;
        [SerializeField] private Button _exitBtn;

        private event Action<float> OnKnownKanjiNumberChange;

        private void Start()
        {
            _kanjiListBtn.onClick.AddListener(Hide);
            _oralTypePanelView.SubscribeToReadingButton(Hide);
            _oralTypePanelView.SubscribeToMeaningButton(Hide);
            _exitBtn.onClick.AddListener(Application.Quit);

            OnKnownKanjiNumberChange += _oralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownKanjiNumberChange += _writingTypePanelView.ChangeQuestionsMaxNumber;
        }

        public void Init(int knownKanjiNum, int lastOralQuestionsNum, int lastWritingQuestionsNum)
        {
            _oralTypePanelView.Init(knownKanjiNum, lastOralQuestionsNum);
            _writingTypePanelView.Init(knownKanjiNum, lastWritingQuestionsNum);
        }

        public void SubscribeOralQuestionsChange(Action<int> callback) => _oralTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeOralQuestionsChange(Action<int> callback) => _oralTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeWritingQuestionsChange(Action<int> callback) => _writingTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeWritingQuestionsChange(Action<int> callback) => _writingTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeToKanjiListButton(Action callback) => _kanjiListBtn.onClick.AddListener(() => callback());

        public void SubscribeToReadingButton(Action callback) => _oralTypePanelView.SubscribeToReadingButton(callback);

        public void SubscribeToMeaningButton(Action callback) => _oralTypePanelView.SubscribeToMeaningButton(callback);

        public void OnKnownKanjiChange(List<string> knownKanjiList) => OnKnownKanjiNumberChange?.Invoke(knownKanjiList.Count);

        private void OnDestroy()
        {
            _kanjiListBtn.onClick.RemoveAllListeners();
            _exitBtn.onClick.RemoveAllListeners();
            OnKnownKanjiNumberChange -= _oralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownKanjiNumberChange -= _writingTypePanelView.ChangeQuestionsMaxNumber;
        }
    }
}
