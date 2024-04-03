using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class ReadingToKanjiView : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;
        [SerializeField] private TextMeshProUGUI _secondQuestionText;
        [SerializeField] private TextMeshProUGUI _answerText;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _answerText.text = kanjiToReadingStruct.Kanji.Kanji;
            _firstQuestionText.text = kanjiToReadingStruct.Kanji.UpperReading;
            _secondQuestionText.text = kanjiToReadingStruct.Kanji.LowerReading;
            _questionNum.text = index.ToString();
        }

        public override void ShowAnswer(Color color)
        {
            _answerText.color = color;
        }

        public override void HideAnswer(Color color)
        {
            _answerText.color = color;
        }
    }
}
