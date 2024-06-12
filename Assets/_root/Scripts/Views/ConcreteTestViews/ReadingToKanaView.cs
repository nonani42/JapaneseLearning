using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class ReadingToKanaView : TestView
    {
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _firstAnswerText;
        [SerializeField] private TextMeshProUGUI _secondAnswerText;

        public override void NextQuestion(TestKanaStruct kanaToReadingStruct, int index)
        {
            _questionText.text = kanaToReadingStruct.Kana.Reading;
            _firstAnswerText.text = kanaToReadingStruct.Kana.HiraganaKeyName;
            _secondAnswerText.text = kanaToReadingStruct.Kana.KatakanaKeyName;
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
