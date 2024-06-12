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

        [SerializeField] private ObjectTypePanelView _kanjiTypePanelView;
        [SerializeField] private TestTypePanelView _kanaTypePanelView;
        [SerializeField] private TestTypePanelView _keyTypePanelView;

        [SerializeField] private Button _oralPanelBtn;
        [SerializeField] private Button _writingPanelBtn;

        [SerializeField] private TestTypePanelView _kanjiOralTypePanelView;
        [SerializeField] private TestTypePanelView _kanjiWritingTypePanelView;


        private event Action<float> OnKnownKanjiNumberChange;

        private void Start()
        {
            SubscribeTopLevelPanels();
            SubscribeLowerLevelPanels();
            SubscribeRightPanel();
            SubscribeLeftPanel();
        }

        public void Init(int knownKanjiNum, int lastOralQuestionsNum, int lastWritingQuestionsNum, 
                        int knownKanaNum, int lastKanaQuestionsNum, 
                        int knownKeyNum, int lastKeyQuestionsNum, 
                        string login)
        {
            _kanjiTypePanelView.Init();
            _kanjiOralTypePanelView.Init(knownKanjiNum, lastOralQuestionsNum);
            _kanjiWritingTypePanelView.Init(knownKanjiNum, lastWritingQuestionsNum);
            _kanaTypePanelView.Init(knownKanaNum, lastKanaQuestionsNum);
            _keyTypePanelView.Init(knownKeyNum, lastKeyQuestionsNum);
            _login.text = login;
        }

        public void SubscribeOralQuestionsChange(Action<int> callback) => _kanjiOralTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeOralQuestionsChange(Action<int> callback) => _kanjiOralTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeWritingQuestionsChange(Action<int> callback) => _kanjiWritingTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeWritingQuestionsChange(Action<int> callback) => _kanjiWritingTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeKanaQuestionsChange(Action<int> callback) => _kanaTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeKanaQuestionsChange(Action<int> callback) => _kanaTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeKeyQuestionsChange(Action<int> callback) => _keyTypePanelView.OnQuestionsNumberChange += (t) => callback(t);
        public void UnsubscribeKeyQuestionsChange(Action<int> callback) => _keyTypePanelView.OnQuestionsNumberChange -= (t) => callback(t);

        public void SubscribeToKanjiListButton(Action callback) => _kanjiListBtn.onClick.AddListener(() => callback());
        public void UnsubscribeToKanjiListButton(Action callback) => _kanjiListBtn.onClick.RemoveListener(() => callback());

        public void SubscribeToOralTestButton(Action[] callback, string buttonName) => _kanjiOralTypePanelView.SubscribeTestToTestButton(callback, buttonName);
        public void SubscribeToWritingTestButton(Action[] callback, string buttonName) => _kanjiWritingTypePanelView.SubscribeTestToTestButton(callback, buttonName);

        public void SubscribeToKanaTestButton(Action[] callback, string buttonName) => _kanaTypePanelView.SubscribeTestToTestButton(callback, buttonName);
        public void SubscribeToKeyTestButton(Action[] callback, string buttonName) => _keyTypePanelView.SubscribeTestToTestButton(callback, buttonName);

        public void OnKnownKanjiChange(List<char> knownKanjiList) => OnKnownKanjiNumberChange?.Invoke(knownKanjiList.Count);

        private void SubscribeLeftPanel()
        {
            _kanjiListBtn.onClick.AddListener(_leftPanel.Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _kanaTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _keyTypePanelView.OnTestButtonClicked += _leftPanel.Hide;
            _exitBtn.onClick.AddListener(Application.Quit);
        }

        private void SubscribeRightPanel()
        {
            _kanjiListBtn.onClick.AddListener(Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked += Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked += Hide;
            _kanaTypePanelView.OnTestButtonClicked += Hide;
            _keyTypePanelView.OnTestButtonClicked += Hide;
        }

        private void SubscribeTopLevelPanels()
        {
            _kanjiPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_kanjiTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_kanaTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.AddListener(_keyTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.AddListener(_kanaTypePanelView.Hide);
            _kanjiPanelBtn.onClick.AddListener(_keyTypePanelView.Hide);

            _kanaPanelBtn.onClick.AddListener(_kanjiTypePanelView.Hide);
            _kanaPanelBtn.onClick.AddListener(_keyTypePanelView.Hide);

            _keyPanelBtn.onClick.AddListener(_kanjiTypePanelView.Hide);
            _keyPanelBtn.onClick.AddListener(_kanaTypePanelView.Hide);
        }

        private void SubscribeLowerLevelPanels()
        {
            _oralPanelBtn.onClick.AddListener(_kanjiOralTypePanelView.ChangeVisibility);
            _writingPanelBtn.onClick.AddListener(_kanjiWritingTypePanelView.ChangeVisibility);
            _oralPanelBtn.onClick.AddListener(_kanjiWritingTypePanelView.Hide);
            _writingPanelBtn.onClick.AddListener(_kanjiOralTypePanelView.Hide);

            OnKnownKanjiNumberChange += _kanjiOralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownKanjiNumberChange += _kanjiWritingTypePanelView.ChangeQuestionsMaxNumber;
        }

        private void UnsubscribeLeftPanel()
        {
            _kanjiListBtn.onClick.RemoveListener(_leftPanel.Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _kanaTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _keyTypePanelView.OnTestButtonClicked -= _leftPanel.Hide;
            _exitBtn.onClick.RemoveListener(Application.Quit);
        }

        private void UnsubscribeRightPanel()
        {
            _kanjiListBtn.onClick.RemoveListener(Hide);
            _kanjiOralTypePanelView.OnTestButtonClicked -= Hide;
            _kanjiWritingTypePanelView.OnTestButtonClicked -= Hide;
            _kanaTypePanelView.OnTestButtonClicked -= Hide;
            _keyTypePanelView.OnTestButtonClicked -= Hide;
        }

        private void UnsubscribeTopLevelPanels()
        {
            _kanjiPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_kanaTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);
            _kanaPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);
            _keyPanelBtn.onClick.RemoveListener(_keyTypePanelView.ChangeVisibility);

            _kanjiPanelBtn.onClick.RemoveListener(_kanaTypePanelView.Hide);
            _kanjiPanelBtn.onClick.RemoveListener(_keyTypePanelView.Hide);

            _kanaPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.Hide);
            _kanaPanelBtn.onClick.RemoveListener(_keyTypePanelView.Hide);

            _keyPanelBtn.onClick.RemoveListener(_kanjiTypePanelView.Hide);
            _keyPanelBtn.onClick.RemoveListener(_kanaTypePanelView.Hide);
        }

        private void UnsubscribeLowerLevelPanels()
        {
            _oralPanelBtn.onClick.RemoveListener(_kanjiOralTypePanelView.ChangeVisibility);
            _writingPanelBtn.onClick.RemoveListener(_kanjiWritingTypePanelView.ChangeVisibility);
            _oralPanelBtn.onClick.RemoveListener(_kanjiWritingTypePanelView.Hide);
            _writingPanelBtn.onClick.RemoveListener(_kanjiOralTypePanelView.Hide);

            OnKnownKanjiNumberChange -= _kanjiOralTypePanelView.ChangeQuestionsMaxNumber;
            OnKnownKanjiNumberChange -= _kanjiWritingTypePanelView.ChangeQuestionsMaxNumber;
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
