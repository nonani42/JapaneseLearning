﻿using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class KanjiToReadingView : TestView
    {
        [SerializeField] private TextMeshProUGUI _kanjiText;
        [SerializeField] private TextMeshProUGUI _upperReadingText;
        [SerializeField] private TextMeshProUGUI _lowerReadingText;

        public override void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index)
        {
            _kanjiText.text = kanjiToReadingStruct.Kanji.Kanji;
            _upperReadingText.text = kanjiToReadingStruct.Kanji.UpperReading;
            _lowerReadingText.text = kanjiToReadingStruct.Kanji.LowerReading;
            _questionNum.text = index.ToString();
        }

        public override void ShowReading(Color color)
        {
            _upperReadingText.color = color;
            _lowerReadingText.color = color;
        }

        public override void HideReading(Color color)
        {
            _upperReadingText.color = color;
            _lowerReadingText.color = color;
        }
    }
}