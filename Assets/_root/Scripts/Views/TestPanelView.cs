using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    public abstract class TestPanelView : PanelView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private TextMeshProUGUI _nextButtonText;

        protected Color showAnswerColor = Color.white;
        protected Color hideAnswerColor = new(0, 0, 0, 0);

        protected Action getNext;
        protected Action showAnswer;

        private bool isTestFinished;

        public bool IsTestFinished 
        {
            get
            {
                return isTestFinished;
            }
            set
            {
                isTestFinished = value;
                _nextButton.interactable = !isTestFinished;
            }
        }

        public event Action OnBack;

        protected void Init()
        {
            _backButton.onClick.AddListener(() => OnBack?.Invoke());
            IsTestFinished = false;
        }

        public void GetNext()
        {
            _nextButtonText.text = "Show answer";
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(()=> showAnswer());
            _nextButton.onClick.AddListener(ShowAnswer);
        }

        private void ShowAnswer()
        {
            _nextButtonText.text = "Next";
            _nextButton.onClick.RemoveAllListeners();
            _nextButton.onClick.AddListener(() => getNext());
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
            _nextButton.onClick.RemoveAllListeners();
        }
    }
}
