﻿using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class KanjiToMeaningView : TestView
    {
        [SerializeField] private TextMeshProUGUI _kanjiText;
        [SerializeField] private TextMeshProUGUI _engMeaningText;
        [SerializeField] private TextMeshProUGUI _rusMeaningText;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _kanjiText.text = kanjiToReadingStruct.Kanji.Kanji;
            _engMeaningText.text = kanjiToReadingStruct.Kanji.MeaningEng;
            _rusMeaningText.text = kanjiToReadingStruct.Kanji.MeaningRus;
            _questionNum.text = index.ToString();
        }

        public override void ShowReading(Color color)
        {
            _engMeaningText.color = color;
            _rusMeaningText.color = color;
        }

        public override void HideReading(Color color)
        {
            _engMeaningText.color = color;
            _rusMeaningText.color = color;
        }
    }
}