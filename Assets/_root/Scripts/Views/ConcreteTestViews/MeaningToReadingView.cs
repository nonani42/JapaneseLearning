using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class MeaningToReadingView : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;
        [SerializeField] private TextMeshProUGUI _secondQuestionText;

        [SerializeField] private TextMeshProUGUI _firstAnswerText;
        [SerializeField] private TextMeshProUGUI _secondAnswerText;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _firstAnswerText.text = kanjiToReadingStruct.Kanji.UpperReading;
            _secondAnswerText.text = kanjiToReadingStruct.Kanji.LowerReading;

            _firstQuestionText.text = kanjiToReadingStruct.Kanji.MeaningEng;
            _secondQuestionText.text = kanjiToReadingStruct.Kanji.MeaningRus;

            _questionNum.text = index.ToString();
        }

        public override void ShowAnswer(Color color)
        {
            _firstAnswerText.color = color;
            _secondAnswerText.color = color;
        }

        public override void HideAnswer(Color color)
        {
            _firstAnswerText.color = color;
            _secondAnswerText.color = color;
        }
    }
}
