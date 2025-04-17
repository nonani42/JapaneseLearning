using System;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    public class TestQuestionPanelView : TestPanelView
    {
        [SerializeField] private TestView _view;
        [SerializeField] private Toggle _repeatToggle;

        private int _index = 0;
        private bool _isLast;
        private TestObjectEnum _testObject;
        protected string _currentQuestion;

        public TestView View { get => _view; }
        public TestObjectEnum TestObject { get => _testObject; set => _testObject = value; }

        public event Func<TestKanjiStruct> OnNextKanji;
        public event Func<TestWordStruct> OnNextWord;
        public event Func<TestKanaStruct> OnNextKana;
        public event Func<TestKeyStruct> OnNextKey;

        public event Action<bool, string> Repeat;


        public new void Init()
        {
            _index = 0;
            base.Init();
            _view.HideAnswer(hideAnswerColor);
            getNext = GetNextQuestion;
            showAnswer = ShowReading;
            _repeatToggle.onValueChanged.AddListener((state) => Repeat?.Invoke(state, _currentQuestion));
        }

        private void SetNextKanji(TestKanjiStruct kanjiStruct)
        {
            _index++;
            _view.NextQuestion(kanjiStruct, _index);
            _isLast = kanjiStruct.IsLast;
            _currentQuestion = kanjiStruct.Kanji.Kanji.ToString();
            _repeatToggle.isOn = kanjiStruct.IsRepeat;
        }

        private void SetNextWord(TestWordStruct wordStruct)
        {
            _index++;
            _view.NextQuestion(wordStruct, _index);
            _isLast = wordStruct.IsLast;
            _currentQuestion = wordStruct.Word.JpReading;
            _repeatToggle.isOn = wordStruct.IsRepeat;
        }

        private void SetNextKana(TestKanaStruct kanaStruct)
        {
            _index++;
            _view.NextQuestion(kanaStruct, _index);
            _isLast = kanaStruct.IsLast;
            _currentQuestion = kanaStruct.Kana.Reading;
            _repeatToggle.gameObject.SetActive(false);
        }

        private void SetNextKey(TestKeyStruct keyStruct)
        {
            _index++;
            _view.NextQuestion(keyStruct, _index);
            _isLast = keyStruct.IsLast;
            _currentQuestion = keyStruct.Key.Reading;
            _repeatToggle.gameObject.SetActive(false);
        }

        public void GetNextQuestion()
        {
            if(_testObject == TestObjectEnum.Kanji)
                SetNextKanji(OnNextKanji.Invoke());
            else if (_testObject == TestObjectEnum.Word)
                SetNextWord(OnNextWord.Invoke());
            else if (_testObject == TestObjectEnum.Kana)
                SetNextKana(OnNextKana.Invoke());
            else if (_testObject == TestObjectEnum.Key)
                SetNextKey(OnNextKey.Invoke());

            _view.HideAnswer(hideAnswerColor);
            GetNext();
        }

        private void ShowReading()
        {
            _view.ShowAnswer(showAnswerColor);
            IsTestFinished = _isLast;
        }

        private void OnDestroy()
        {
            _repeatToggle.onValueChanged.RemoveListener((state) => Repeat?.Invoke(state, _currentQuestion));
        }
    }
}
