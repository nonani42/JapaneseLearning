using System.Collections.Generic;

namespace TestSpace
{
    public class TestController : IController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private TestModel _testModel;

        public TestController(TestQuestionPanelView kanjiToReadingPanelView, KanjiSO[] kanjiArr, List<string> knownKanjiList)
        {
            _testModel = new(kanjiArr, knownKanjiList);
            _testQuestionPanelView = kanjiToReadingPanelView;
            _testQuestionPanelView.OnNextQuestion += _testModel.NextKanji;
        }

        public void Init()
        {
            _testModel.Init();
            _testQuestionPanelView.Init();
            _testQuestionPanelView.GetNextQuestion();
        }

        public void SetTestLength(int testLength) => _testModel.SetTestLength(testLength);
        public void SetKnownKanji(List<string> knownKanjiList) => _testModel.SetKnownKanji(knownKanjiList);

        public void Destroy()
        {
            _testQuestionPanelView.OnNextQuestion -= _testModel.NextKanji;
            _testModel.Destroy();
        }
    }
}
