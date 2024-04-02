using System;
using UnityEngine;

namespace TestSpace
{
    public class TestQuestionPanelView : TestPanelView
    {
        [SerializeField] private TestView _view;

        private int _index = 0;
        private bool _isLast;

        public event Func<TestKanjiStruct> OnNextQuestion;

        public new void Init()
        {
            _index = 0;
            base.Init();
            _view.Init();
            _view.HideReading(hideAnswerColor);
            getNext = GetNextQuestion;
            showAnswer = ShowReading;
        }

        private void SetNextKanji(TestKanjiStruct kanjiToReadingStruct)
        {
            _index++;
            _view.NextQuestion(kanjiToReadingStruct, _index);
            _isLast = kanjiToReadingStruct.IsLast;
        }

        public void GetNextQuestion()
        {
            SetNextKanji(OnNextQuestion.Invoke());
            _view.HideReading(hideAnswerColor);
            GetNext();
        }

        private void ShowReading()
        {
            _view.ShowReading(showAnswerColor);
            IsTestFinished = _isLast;
        }
    }
}
