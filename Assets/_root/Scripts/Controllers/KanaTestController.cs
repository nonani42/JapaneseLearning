using System;
using System.Diagnostics;

namespace TestSpace
{
    internal class KanaTestController : ITestController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private KanaTestModel _testModel;

        public event Action<TestObjectEnum, string, bool> TestObjectRepeat;

        public KanaTestController(TestQuestionPanelView kanjiToReadingPanelView, KanaSO[] kanaArr)
        {
            _testModel = new(kanaArr);
            _testQuestionPanelView = kanjiToReadingPanelView;
            _testQuestionPanelView.OnNextKana += _testModel.NextKana;
        }

        public void Init()
        {
            _testModel.Init();
            _testQuestionPanelView.Init();
            _testQuestionPanelView.GetNextQuestion();
        }

        public void SetTestLength(int testLength) => _testModel.SetTestLength(testLength);

        public void Destroy()
        {
            _testQuestionPanelView.OnNextKana -= _testModel.NextKana;
            _testModel.Destroy();
        }
    }
}
