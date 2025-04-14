using System.Collections.Generic;

namespace TestSpace
{
    public class WordTestController : ITestController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private WordTestModel _testModel;

        public WordTestController(TestQuestionPanelView testPanelView, WordSO[] allWordsList, List<string> knownWordsList)
        {
            _testModel = new(allWordsList, knownWordsList);
            _testQuestionPanelView = testPanelView;
            _testQuestionPanelView.OnNextWord += _testModel.NextWord;
        }

        public void Init()
        {
            _testModel.Init();
            _testQuestionPanelView.Init();
            _testQuestionPanelView.GetNextQuestion();
        }

        public void SetTestLength(int testLength) => _testModel.SetTestLength(testLength);
        public void SetKnownWord(List<string> knownWordsList) => _testModel.SetKnownWord(knownWordsList);

        public void Destroy()
        {
            _testQuestionPanelView.OnNextWord -= _testModel.NextWord;
            _testModel.Destroy();
        }
    }
}
