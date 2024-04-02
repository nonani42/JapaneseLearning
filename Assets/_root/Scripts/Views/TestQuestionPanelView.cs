using System;
using UnityEngine;

namespace TestSpace
{
    public class TestQuestionPanelView : TestPanelView
    {
        [SerializeField] private TestView _view;

        private int _index = 0;
        private bool _isLast;

        public TestView View { get => _view; }

        public event Func<TestKanjiStruct> OnNextQuestion;

        public new void Init()
        {
            _index = 0;
            base.Init();
            _view.HideAnswer(hideAnswerColor);
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
            _view.HideAnswer(hideAnswerColor);
            GetNext();
        }

        private void ShowReading()
        {
            _view.ShowAnswer(showAnswerColor);
            IsTestFinished = _isLast;
        }
    }
}
