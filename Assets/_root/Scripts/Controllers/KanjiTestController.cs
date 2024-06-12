using System.Collections.Generic;

namespace TestSpace
{
    public class KanjiTestController : ITestController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private KanjiTestModel _testModel;

        public KanjiTestController(TestQuestionPanelView kanjiToReadingPanelView, KanjiCardSO[] kanjiArr, List<char> knownKanjiList)
        {
            _testModel = new(kanjiArr, knownKanjiList);
            _testQuestionPanelView = kanjiToReadingPanelView;
            _testQuestionPanelView.OnNextKanji += _testModel.NextKanji;
        }

        public void Init()
        {
            _testModel.Init();
            _testQuestionPanelView.Init();
            _testQuestionPanelView.GetNextQuestion();
        }

        public void SetTestLength(int testLength) => _testModel.SetTestLength(testLength);
        public void SetKnownKanji(List<char> knownKanjiList) => _testModel.SetKnownKanji(knownKanjiList);

        public void Destroy()
        {
            _testQuestionPanelView.OnNextKanji -= _testModel.NextKanji;
            _testModel.Destroy();
        }
    }
}
