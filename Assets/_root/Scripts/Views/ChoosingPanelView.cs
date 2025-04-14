using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    public class ChoosingPanelView : PanelView
    {
        [SerializeField] private PanelView _leftPanel;

        [SerializeField] private TextMeshProUGUI _login;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Button _kanjiListBtn;
        [SerializeField] private Button _exitBtn;

        [SerializeField] private Button _kanjiPanelBtn;
        [SerializeField] private Button _kanaPanelBtn;
        [SerializeField] private Button _keyPanelBtn;
        [SerializeField] private Button _wordPanelBtn;

        [SerializeField] private ObjectTypePanelView _kanjiTypePanelView;
        [SerializeField] private ObjectTypePanelView _wordTypePanelView;
        [SerializeField] private TestTypePanelView _kanaTypePanelView;
        [SerializeField] private TestTypePanelView _keyTypePanelView;

        [SerializeField] private Button _wordOralPanelBtn;
        [SerializeField] private Button _wordWritingPanelBtn;

        [SerializeField] private Button _kanjiOralPanelBtn;
        [SerializeField] private Button _kanjiWritingPanelBtn;

        [SerializeField] private TestTypePanelView _kanjiOralTypePanelView;
        [SerializeField] private TestTypePanelView _kanjiWritingTypePanelView;

        [SerializeField] private TestTypePanelView _wordOralTypePanelView;
        [SerializeField] private TestTypePanelView _wordWritingTypePanelView;


        private event Action<float> OnKnownKanjiNumberChange;
        private event Action<float> OnKnownWordsNumberChange;

        private void Start()
        {
            SubscribeTopLevelPanels();
            SubscribeLowerLevelPanels();
            SubscribeRightPanel();
            SubscribeLeftPanel();
        }

        public void Init(int knownKanjiNum, int lastKanjiOralQNum, int lastKanjiWritingQNum, 
                        int knownKanaNum, int lastKanaQuestionsNum, 
                        int knownKeyNum, int lastKeyQuestionsNum,
                        int knownWordsNum, int lastWordOralQNum, int lastWordWritingQNum,
                        string login)
        {
            _kanjiTypePanelView.Init();
            _wordTypePanelView.Init();
            _kanjiOralTypePanelView.Init(knownKanjiNum, lastKanjiOralQNum);
            _kanjiWritingTypePanelView.Init(knownKanjiNum, lastKanjiWritingQNum);
            _wordOralTypePanelView.Init(knownWordsNum, lastWordOralQNum);
            _wordWritingTypePanelView.Init(knownWordsNum, lastWordWritingQNum);
            _kanaTypePanelView.Init(knownKanaNum, lastKanaQuestionsNum);
            _keyTypePanelView.Init(knownKeyNum, lastKeyQuestionsNum);
            _login.text = login;
        }

        public void SubscribeKanjiOralQuestionsChange(Action<int> callback) => _kanjiOralTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeKanjiOralQuestionsChange(Action<int> callback) => _kanjiOralTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeKanjiWritingQuestionsChange(Action<int> callback) => _kanjiWritingTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeKanjiWritingQuestionsChange(Action<int> callback) => _kanjiWritingTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeWordOralQuestionsChange(Action<int> callback) => _wordOralTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeWordOralQuestionsChange(Action<int> callback) => _wordOralTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeWordWritingQuestionsChange(Action<int> callback) => _wordWritingTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeWordWritingQuestionsChange(Action<int> callback) => _wordWritingTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeKanaQuestionsChange(Action<int> callback) => _kanaTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeKanaQuestionsChange(Action<int> callback) => _kanaTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeKeyQuestionsChange(Action<int> callback) => _keyTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeKeyQuestionsChange(Action<int> callback) => _keyTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeToKanjiListButton(Action callback) => _kanjiListBtn.onClick.AddListener(() => callback());
        public void UnsubscribeToKanjiListButton(Action callback) => _kanjiListBtn.onClick.RemoveListener(() => callback());

        public void SubscribeToKanjiOralTestButton(Action[] callback, string buttonName) => _kanjiOralTypePanelView.SubscribeTestToTestButton(callback, buttonName);
        public void SubscribeToKanjiWritingTestButton(Action[] callback, string buttonName) => _kanjiWritingTypePanelView.SubscribeTestToTestButton(callback, buttonName);

        public void SubscribeToWordOralTestButton(Action[] callback, string buttonName) => _wordOralTypePanelView.SubscribeTestToTestButton(callback, buttonName);
        public void SubscribeToWordWritingTestButton(Action[] callback, string buttonName) => _wordWritingTypePanelView.SubscribeTestToTestButton(callback, buttonName);

        public void SubscribeToKanaTestButton(Action[] callback, string buttonName) => _kanaTypePanelView.SubscribeTestToTestButton(callback, buttonName);
        public void SubscribeToKeyTestButton(Action[] callback, string buttonName) => _keyTypePanelView.SubscribeTestToTestButton(callback, buttonName);

        public void OnKnownKanjiChange(List<char> knownKanjiList) => OnKnownKanjiNumberChange?.Invoke(knownKanjiList.Count);

        public void OnKnownWordsChange(List<string> knownWordsList) => OnKnownWordsNumberChange?.Invoke(knownWordsList.Count);

        private void SubscribeLeftPanel()
        {
            _kanjiListBtn.onClick.AddListener(_leftPanel.Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _wordOralTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _wordWritingTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _kanaTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _keyTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _exitBtn.onClick.AddListener(Application.Quit);
        }

        private void SubscribeRightPanel()
        {
            _kanjiListBtn.onClick.AddListener(Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked += Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked += Hide;
            _wordOralTypePanelView.OnTestButtonClicked += Hide;
            _wordWritingTypePanelView.OnTestButtonClicked += Hide;
            _kanaTypePanelView.OnTestButtonClicked += Hide;
            _keyTypePanelView.OnTestButtonClicked += Hide;
        }

        private void SubscribeTopLevelPanels()
        {
            _kanjiPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_wordTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.AddListener(_wordTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_wordTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_wordTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_kanaTypePanelView.Hide);
            _kanjiPanelBtn.onClick.AddListener(_keyTypePanelView.Hide);
            _kanjiPanelBtn.onClick.AddListener(_wordTypePanelView.Hide);

            _wordPanelBtn.onClick.AddListener(_kanaTypePanelView.Hide);
            _wordPanelBtn.onClick.AddListener(_keyTypePanelView.Hide);
            _wordPanelBtn.onClick.AddListener(_kanjiTypePanelView.Hide);

            _kanaPanelBtn.onClick.AddListener(_kanjiTypePanelView.Hide);
            _kanaPanelBtn.onClick.AddListener(_keyTypePanelView.Hide);
            _kanaPanelBtn.onClick.AddListener(_wordTypePanelView.Hide);

            _keyPanelBtn.onClick.AddListener(_kanjiTypePanelView.Hide);
            _keyPanelBtn.onClick.AddListener(_kanaTypePanelView.Hide);
            _keyPanelBtn.onClick.AddListener(_wordTypePanelView.Hide);
        }

        private void SubscribeLowerLevelPanels()
        {
            _kanjiOralPanelBtn.onClick.AddListener(_kanjiOralTypePanelView.ChangeVisibility);
            _kanjiWritingPanelBtn.onClick.AddListener(_kanjiWritingTypePanelView.ChangeVisibility);
            _kanjiOralPanelBtn.onClick.AddListener(_kanjiWritingTypePanelView.Hide);
            _kanjiWritingPanelBtn.onClick.AddListener(_kanjiOralTypePanelView.Hide);

            _wordOralPanelBtn.onClick.AddListener(_wordOralTypePanelView.ChangeVisibility);
            _wordWritingPanelBtn.onClick.AddListener(_wordWritingTypePanelView.ChangeVisibility);
            _wordOralPanelBtn.onClick.AddListener(_wordWritingTypePanelView.Hide);
            _wordWritingPanelBtn.onClick.AddListener(_wordOralTypePanelView.Hide);

            OnKnownKanjiNumberChange += _kanjiOralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownKanjiNumberChange += _kanjiWritingTypePanelView.ChangeQuestionsMaxNumber;

            OnKnownWordsNumberChange += _wordOralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownWordsNumberChange += _wordWritingTypePanelView.ChangeQuestionsMaxNumber;
        }

        private void UnsubscribeLeftPanel()
        {
            _kanjiListBtn.onClick.RemoveListener(_leftPanel.Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _wordOralTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _wordWritingTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _kanaTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _keyTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _exitBtn.onClick.RemoveListener(Application.Quit);
        }

        private void UnsubscribeRightPanel()
        {
            _kanjiListBtn.onClick.RemoveListener(Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked -= Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked -= Hide;
            _wordOralTypePanelView.OnTestButtonClicked -= Hide;
            _wordWritingTypePanelView.OnTestButtonClicked -= Hide;
            _kanaTypePanelView.OnTestButtonClicked -= Hide;
            _keyTypePanelView.OnTestButtonClicked -= Hide;
        }

        private void UnsubscribeTopLevelPanels()
        {
            _kanjiPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_wordTypePanelView.ChangeVisibility);
            _wordPanelBtn.onClick.RemoveListener(_wordTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_wordTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_wordTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_kanaTypePanelView.Hide);
            _kanjiPanelBtn.onClick.RemoveListener(_keyTypePanelView.Hide);
            _kanjiPanelBtn.onClick.RemoveListener(_wordTypePanelView.Hide);

            _wordPanelBtn.onClick.RemoveListener(_kanaTypePanelView.Hide);
            _wordPanelBtn.onClick.RemoveListener(_keyTypePanelView.Hide);
            _wordPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.Hide);

            _kanaPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.Hide);
            _kanaPanelBtn.onClick.RemoveListener(_keyTypePanelView.Hide);
            _kanaPanelBtn.onClick.RemoveListener(_wordTypePanelView.Hide);

            _keyPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.Hide);
            _keyPanelBtn.onClick.RemoveListener(_kanaTypePanelView.Hide);
            _keyPanelBtn.onClick.RemoveListener(_wordTypePanelView.Hide);
        }

        private void UnsubscribeLowerLevelPanels()
        {
            _kanjiOralPanelBtn.onClick.RemoveListener(_kanjiOralTypePanelView.ChangeVisibility);
            _kanjiWritingPanelBtn.onClick.RemoveListener(_kanjiWritingTypePanelView.ChangeVisibility);
            _kanjiOralPanelBtn.onClick.RemoveListener(_kanjiWritingTypePanelView.Hide);
            _kanjiWritingPanelBtn.onClick.RemoveListener(_kanjiOralTypePanelView.Hide);

            OnKnownKanjiNumberChange -= _kanjiOralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownKanjiNumberChange -= _kanjiWritingTypePanelView.ChangeQuestionsMaxNumber;

            OnKnownWordsNumberChange -= _wordOralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownWordsNumberChange -= _wordWritingTypePanelView.ChangeQuestionsMaxNumber;
        }

        private void OnDestroy()
        {
            UnsubscribeLeftPanel();
            UnsubscribeRightPanel();
            UnsubscribeTopLevelPanels();
            UnsubscribeLowerLevelPanels();
        }
    }
}
