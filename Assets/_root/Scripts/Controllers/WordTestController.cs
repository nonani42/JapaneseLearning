using System;
using System.Collections.Generic;

namespace TestSpace
{
    public class WordTestController : ITestController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private WordTestModel _testModel;

        public event Action<TestObjectEnum, string, bool> TestObjectRepeat;

        public WordTestController(TestQuestionPanelView testPanelView, WordSO[] allWordsList, List<string> knownWordsList, List<string> repeatWordsList)
        {
            _testModel = new(allWordsList, knownWordsList, repeatWordsList);
            _testQuestionPanelView = testPanelView;
            _testQuestionPanelView.OnNextWord += _testModel.NextWord;
        }

        public void Init()
        {
            _testModel.Init();
            _testQuestionPanelView.Init();
            _testQuestionPanelView.GetNextQuestion();
            _testQuestionPanelView.Repeat += (st, ob) => TestObjectRepeat?.Invoke(TestObjectEnum.Word, ob, st);
        }

        public void SetTestLength(int testLength) => _testModel.SetTestLength(testLength);

        public void SetKnownWord(List<string> knownWordsList) => _testModel.SetKnownWord(knownWordsList);

        public void Destroy()
        {
            _testQuestionPanelView.OnNextWord -= _testModel.NextWord;
            _testQuestionPanelView.Repeat -= (st, ob) => TestObjectRepeat?.Invoke(TestObjectEnum.Word, ob, st);

            _testModel.Destroy();
        }
    }
}
