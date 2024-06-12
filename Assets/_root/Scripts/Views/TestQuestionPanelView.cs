using System;
using UnityEngine;

namespace TestSpace
{
    public class TestQuestionPanelView : TestPanelView
    {
        [SerializeField] private TestView _view;

        private int _index = 0;
        private bool _isLast;
        private TestObjectEnum _testObject;

        public TestView View { get => _view; }
        public TestObjectEnum TestObject { get => _testObject; set => _testObject = value; }

        public event Func<TestKanjiStruct> OnNextKanji;
        public event Func<TestKanaStruct> OnNextKana;

        public new void Init()
        {
            _index = 0;
            base.Init();
            _view.HideAnswer(hideAnswerColor);
            getNext = GetNextQuestion;
            showAnswer = ShowReading;
        }

        private void SetNextKanji(TestKanjiStruct kanjiStruct)
        {
            _index++;
            _view.NextQuestion(kanjiStruct, _index);
            _isLast = kanjiStruct.IsLast;
        }

        private void SetNextKana(TestKanaStruct kanjiStruct)
        {
            _index++;
            _view.NextQuestion(kanjiStruct, _index);
            _isLast = kanjiStruct.IsLast;
        }

        public void GetNextQuestion()
        {
            if(_testObject == TestObjectEnum.Kanji)
                SetNextKanji(OnNextKanji.Invoke());
            else if (_testObject == TestObjectEnum.Kana)
                SetNextKana(OnNextKana.Invoke());

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
