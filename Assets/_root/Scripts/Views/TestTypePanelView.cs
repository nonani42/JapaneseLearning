using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class TestTypePanelView : PanelView
    {
        [SerializeField] private Button[] _btnList;
        [SerializeField] private Slider _questionsNumberSlider;
        [SerializeField] private TextMeshProUGUI _questionsNumberText;

        private Dictionary<Button, bool> _btnDic;

        protected Dictionary<Button, bool> BtnDic { 
            get 
            {
                if (_btnDic == null)
                {
                    _btnDic = new Dictionary<Button, bool>();

                    for (int i = 0; i < _btnList.Length; i++)
                        _btnDic.Add(_btnList[i], false);
                }

                return _btnDic; 
            } 
            private set => _btnDic = value; }

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
            for (int i = 0; i < _btnList.Length; i++)
            {
                _btnList[i].interactable = value > 0;
            }
            _questionsNumberText.text = value.ToString();
            OnQuestionsNumberChange?.Invoke((int)value);
        }

        public void SubscribeTestToTestButton(Action[] callback, string buttonName)
        {
            var btn = BtnDic.Where(b => b.Value == false).FirstOrDefault();

            for (int i = 0; i < callback.Length; i++)
            {
                var temp = callback[i];
                btn.Key.onClick.AddListener(() => temp());
            }
            btn.Key.GetComponentInChildren<TextMeshProUGUI>().text = buttonName;
            BtnDic[btn.Key] = true;
        }

        public void SubscribeButtons(Action callback)
        {
            if (_btnList.Length == 0)
                return;

            for (int i = 0; i < _btnList.Length; i++)
                _btnList[i].onClick.AddListener(() => callback());
        }

        private void OnDestroy()
        {
            _questionsNumberSlider.onValueChanged.RemoveListener(ChangeQuestionsNumber);
            for(int i = 0; i < _btnList.Length; i++)
            {
                _btnList[i].onClick.RemoveAllListeners();
            }
        }
    }
}
