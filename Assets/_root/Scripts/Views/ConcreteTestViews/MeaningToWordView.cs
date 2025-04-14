using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class MeaningToWordView : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;
        [SerializeField] private TextMeshProUGUI _secondQuestionText;

        [SerializeField] private TextMeshProUGUI _firstAnswerText;
        [SerializeField] private TextMeshProUGUI _specialReadingText;

        public override void NextQuestion(TestWordStruct MeaningToWordStruct, int index)
        {
            _firstAnswerText.text = MeaningToWordStruct.Word.JpReading;

            _firstQuestionText.text = MeaningToWordStruct.Word.TranslationEng;
            _secondQuestionText.text = MeaningToWordStruct.Word.TranslationRus;

            _specialReadingText.text = MeaningToWordStruct.Word.SpecialReading ? "Special reading" : "";

            _questionNum.text = index.ToString();
        }

        public override void ShowAnswer(Color color)
        {
            _firstAnswerText.color = color;
            _specialReadingText.color = color;
        }

        public override void HideAnswer(Color color)
        {
            _firstAnswerText.color = color;
            _specialReadingText.color = color;
        }
    }
}
