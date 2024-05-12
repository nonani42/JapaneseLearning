using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class ReadingToKanjiView : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;
        [SerializeField] private TextMeshProUGUI _secondQuestionText;
        [SerializeField] private TextMeshProUGUI _answerText;
        [SerializeField] private Image _strokeOrder;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _answerText.text = kanjiToReadingStruct.Kanji.Kanji.ToString();
            _firstQuestionText.text = kanjiToReadingStruct.Kanji.UpperReading;
            _secondQuestionText.text = kanjiToReadingStruct.Kanji.LowerReading;
            _questionNum.text = index.ToString();
            if (kanjiToReadingStruct.Kanji.StrokeOrder != null)
                _strokeOrder.sprite = kanjiToReadingStruct.Kanji.StrokeOrder;
        }

        public override void ShowAnswer(Color color)
        {
            _answerText.color = color;
            _strokeOrder.enabled = true;
        }

        public override void HideAnswer(Color color)
        {
            _answerText.color = color;
            _strokeOrder.enabled = false;
        }
    }
}
