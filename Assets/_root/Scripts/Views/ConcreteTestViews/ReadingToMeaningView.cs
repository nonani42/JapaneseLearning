using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class ReadingToMeaningView : TestView
    {
        [SerializeField] private TextMeshProUGUI _upperReadingText;
        [SerializeField] private TextMeshProUGUI _lowerReadingText;

        [SerializeField] private TextMeshProUGUI _engMeaningText;
        [SerializeField] private TextMeshProUGUI _rusMeaningText;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _upperReadingText.text = kanjiToReadingStruct.Kanji.UpperReading;
            _lowerReadingText.text = kanjiToReadingStruct.Kanji.LowerReading;

            _engMeaningText.text = kanjiToReadingStruct.Kanji.MeaningEng;
            _rusMeaningText.text = kanjiToReadingStruct.Kanji.MeaningRus;

            _questionNum.text = index.ToString();
        }

        public override void ShowAnswer(Color color)
        {
            _engMeaningText.color = color;
            _rusMeaningText.color = color;
        }

        public override void HideAnswer(Color color)
        {
            _engMeaningText.color = color;
            _rusMeaningText.color = color;
        }
    }
}
