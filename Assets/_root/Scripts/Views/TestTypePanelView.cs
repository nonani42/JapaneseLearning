using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class TestTypePanelView : PanelView
    {
        [SerializeField] private Button _btnPrefab;
        [SerializeField] private Transform _btnHolderTransform;
        [SerializeField] private Slider _questionsNumberSlider;
        [SerializeField] private TextMeshProUGUI _questionsNumberText;

        private List<Button> _btnList = new();

        public event Action<int> OnQuestionsNumberChange;
        public event Action OnTestButtonClicked;


        public void Init(int kanjiNum, int lastQuestionsNum)
        {
            _questionsNumberSlider.onValueChanged.AddListener(ChangeQuestionsNumber);
            ChangeQuestionsMaxNumber(kanjiNum);
            _questionsNumberSlider.value = lastQuestionsNum;
            Hide();
        }

        public void ChangeQuestionsMaxNumber(float value) => _questionsNumberSlider.maxValue = value;

        private void ChangeQuestionsNumber(float value)
        {
            for (int i = 0; i < _btnList.Count; i++)
            {
                _btnList[i].interactable = value > 0;
            }
            _questionsNumberText.text = value.ToString();
            OnQuestionsNumberChange?.Invoke((int)value);
        }

        public void SubscribeTestToTestButton(Action[] callback, string buttonName)
        {
            var tempButton = Instantiate(_btnPrefab, _btnHolderTransform);

            tempButton.onClick.AddListener(() => OnTestButtonClicked?.Invoke());
            for (int i = 0; i < callback.Length; i++)
            {
                var temp = callback[i];
                tempButton.onClick.AddListener(() => temp());
            }
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;

            _btnList.Add(tempButton);
        }

        public void ChangeVisibility()
        {
            if (this.gameObject.activeSelf)
                Hide();
            else
                Show();
        }

        private void OnDestroy()
        {
            _questionsNumberSlider.onValueChanged.RemoveListener(ChangeQuestionsNumber);
            for(int i = 0; i < _btnList.Count; i++)
            {
                _btnList[i].onClick.RemoveAllListeners();
            }
        }
    }
}
