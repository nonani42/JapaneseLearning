using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class MeaningToKanjiView : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;
        [SerializeField] private TextMeshProUGUI _secondQuestionText;
        [SerializeField] private TextMeshProUGUI _answerText;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _answerText.text = kanjiToReadingStruct.Kanji.Kanji;
            _firstQuestionText.text = kanjiToReadingStruct.Kanji.MeaningEng;
            _secondQuestionText.text = kanjiToReadingStruct.Kanji.MeaningRus;
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
