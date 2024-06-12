namespace TestSpace
{
    internal class KeyTestController : ITestController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private KeyTestModel _testModel;

        public KeyTestController(TestQuestionPanelView kanjiToReadingPanelView, KeySO[] keyArr)
        {
            _testModel = new(keyArr);
            _testQuestionPanelView = kanjiToReadingPanelView;
            _testQuestionPanelView.OnNextKey += _testModel.NextKey;
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
            _testQuestionPanelView.OnNextKey -= _testModel.NextKey;
            _testModel.Destroy();
        }

    }
}
