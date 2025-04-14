using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class WordToEverything : TestView
    {
        [SerializeField] private TextMeshProUGUI _firstQuestionText;

        [SerializeField] private TextMeshProUGUI _firstAnswerText;
        [SerializeField] private TextMeshProUGUI _secondAnswerText;
        [SerializeField] private TextMeshProUGUI _thirdAnswerText;
        [SerializeField] private TextMeshProUGUI _specialReadingText;

        public override void NextQuestion(TestWordStruct MeaningToWordStruct, int index)
        {
            _firstAnswerText.text = MeaningToWordStruct.Word.KanaReading;
            _secondAnswerText.text = MeaningToWordStruct.Word.TranslationEng;
            _thirdAnswerText.text = MeaningToWordStruct.Word.TranslationRus;

            _firstQuestionText.text = MeaningToWordStruct.Word.JpReading;

            _specialReadingText.text = MeaningToWordStruct.Word.SpecialReading ? "Special reading" : "";

            _questionNum.text = index.ToString();
        }

        public override void ShowAnswer(Color color)
        {
            _firstAnswerText.color = color;
            _secondAnswerText.color = color;
            _thirdAnswerText.color = color;
            _specialReadingText.color = color;
        }

        public override void HideAnswer(Color color)
        {
            _firstAnswerText.color = color;
            _secondAnswerText.color = color;
            _thirdAnswerText.color = color;
            _specialReadingText.color = color;
        }
    }
}
