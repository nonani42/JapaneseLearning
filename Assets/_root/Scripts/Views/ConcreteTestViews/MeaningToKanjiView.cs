using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class MeaningToKanjiView : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;
        [SerializeField] private TextMeshProUGUI _secondQuestionText;
        [SerializeField] private Image _strokeOrder;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _firstQuestionText.text = kanjiToReadingStruct.Kanji.MeaningEng;
            _secondQuestionText.text = kanjiToReadingStruct.Kanji.MeaningRus;
            _questionNum.text = index.ToString();
            if(kanjiToReadingStruct.Kanji.StrokeOrder != null)
                _strokeOrder.sprite = kanjiToReadingStruct.Kanji.StrokeOrder;
        }

        public override void ShowAnswer(Color color)
        {
            _strokeOrder.enabled = true;
        }

        public override void HideAnswer(Color color)
        {
            _strokeOrder.enabled = false;
        }
    }
}
