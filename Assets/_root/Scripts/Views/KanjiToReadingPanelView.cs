using System;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace TestSpace
{
    public class KanjiToReadingPanelView : TestPanelView
    {
        [SerializeField] private TextMeshProUGUI _questionNum;
        [SerializeField] private TextMeshProUGUI _kanjiText;
        [SerializeField] private TextMeshProUGUI _upperReadingText;
        [SerializeField] private TextMeshProUGUI _lowerReadingText;

        private int _index = 0;
        private bool _isLast;

        public Func<KanjiToReadingStruct> OnNextKanji;

        public new void Init()
        {
            base.Init();
            HideReading();
            getNext = GetNextKanji;
            showAnswer = ShowReading;
            _index = 0;
            _questionNum.text = _index.ToString();
        }

        public void SetNextKanji(KanjiToReadingStruct kanjiToReadingStruct)
        {
            _kanjiText.text = kanjiToReadingStruct.KanjiText;
            _upperReadingText.text = kanjiToReadingStruct.UpperReadingText;
            _lowerReadingText.text = kanjiToReadingStruct.LowerReadingText;
            _isLast = kanjiToReadingStruct.IsLast;
            _index ++;
            _questionNum.text = _index.ToString();
        }

        public void GetNextKanji()
        {
            SetNextKanji(OnNextKanji.Invoke());
            HideReading();
            GetNext();
        }

        public void ShowReading()
        {
            ShowAnswer();
            _upperReadingText.color = showAnswerColor;
            _lowerReadingText.color = showAnswerColor;
            IsTestFinished = _isLast;
        }

        private void HideReading()
        {
            _upperReadingText.color = hideAnswerColor;
            _lowerReadingText.color = hideAnswerColor;
        }
    }
}
