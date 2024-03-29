using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class TestTypePanelView : PanelView
    {
        [SerializeField] private Button _kanjiToReadingBtn;
        [SerializeField] private Button _kanjiToMeaningBtn;
        [SerializeField] private Slider _questionsNumberSlider;
        [SerializeField] private TextMeshProUGUI _questionsNumberText;

        public event Action<int> OnQuestionsNumberChange;


        public void Init(int kanjiNum, int lastQuestionsNum)
        {
            _questionsNumberSlider.onValueChanged.AddListener(ChangeQuestionsNumber);
            ChangeQuestionsMaxNumber(kanjiNum);
            _questionsNumberSlider.value = lastQuestionsNum;
        }

        public void ChangeQuestionsMaxNumber(float value)
        {
            _questionsNumberSlider.maxValue = value;
        }

        private void ChangeQuestionsNumber(float value)
        {
            _kanjiToReadingBtn.interactable = value > 0;
            _kanjiToMeaningBtn.interactable = value > 0;
            _questionsNumberText.text = value.ToString();
            OnQuestionsNumberChange?.Invoke((int)value);
        }

        public void SubscribeToReadingButton(Action callback) => _kanjiToReadingBtn.onClick.AddListener(() => callback());

        public void SubscribeToMeaningButton(Action callback) => _kanjiToMeaningBtn.onClick.AddListener(() => callback());

        private void OnDestroy()
        {
            _kanjiToReadingBtn.onClick.RemoveAllListeners();
            _kanjiToMeaningBtn.onClick.RemoveAllListeners();
            _questionsNumberSlider.onValueChanged.RemoveListener(ChangeQuestionsNumber);
        }
    }
}
