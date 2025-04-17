using System;
using System.Collections.Generic;

namespace TestSpace
{
    public class KanjiTestController : ITestController
    {
        private TestQuestionPanelView _testQuestionPanelView;
        private KanjiTestModel _testModel;

        public event Action<TestObjectEnum, string, bool> TestObjectRepeat;

        public KanjiTestController(TestQuestionPanelView kanjiToReadingPanelView, KanjiCardSO[] kanjiArr, List<char> knownKanjiList, List<string> repeatKanjiList)
        {
            _testModel = new(kanjiArr, knownKanjiList, repeatKanjiList);
            _testQuestionPanelView = kanjiToReadingPanelView;
            _testQuestionPanelView.OnNextKanji += _testModel.NextKanji;
        }

        public void Init()
        {
            _testModel.Init();
            _testQuestionPanelView.Init();
            _testQuestionPanelView.GetNextQuestion();
            _testQuestionPanelView.Repeat += (st, ob) => TestObjectRepeat?.Invoke(TestObjectEnum.Kanji, ob, st);
        }

        public void SetTestLength(int testLength) => _testModel.SetTestLength(testLength);

        public void SetKnownKanji(List<char> knownKanjiList) => _testModel.SetKnownKanji(knownKanjiList);

        public void Destroy()
        {
            _testQuestionPanelView.OnNextKanji -= _testModel.NextKanji;
            _testQuestionPanelView.Repeat -= (st, ob) => TestObjectRepeat?.Invoke(TestObjectEnum.Word, ob, st);

            _testModel.Destroy();
        }
    }
}
