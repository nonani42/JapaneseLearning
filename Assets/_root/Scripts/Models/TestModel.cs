using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSpace
{
    public struct TestKanjiStruct
    {
        public KanjiSO Kanji;
        public bool IsLast;
    }

    internal class TestModel
    {
        private KanjiSO[] _allKanjiArr;
        private List<string> _knownKanjiList = new List<string>();
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedKanjiNumbers = new List<int>();
        private Random _random = new Random();

        public TestModel(KanjiSO[] allKanjiArr, List<string> knownKanjiList)
        {
            _allKanjiArr = allKanjiArr;
            _knownKanjiList = knownKanjiList;
        }

        public void Init()
        {
            _testedKanjiNumbers.Clear();
            _currentTestQuestion = 0;
        }

        public void SetTestLength(int testLength) => _testLength = testLength;
        public void SetKnownKanji(List<string> knownKanjiList) => _knownKanjiList = knownKanjiList;

        private int GetRandomIndex()
        {
            int newKanjiNum;

            do
            {
                newKanjiNum = _random.Next(_knownKanjiList.Count);
            } while (_testedKanjiNumbers.Contains(newKanjiNum));

            _testedKanjiNumbers.Add(newKanjiNum);
            _currentTestQuestion++;
            return newKanjiNum;
        }

        public TestKanjiStruct NextKanji() => GetNextKanji(_knownKanjiList[GetRandomIndex()]);

        private TestKanjiStruct GetNextKanji(string kanjiName)
        {
            int index = _allKanjiArr.Select((kanji, index) => new { kanji, index }).Where(k => k.kanji.Kanji == kanjiName).FirstOrDefault().index;
            TestKanjiStruct kanjiStruct = new TestKanjiStruct()
            {
                Kanji = _allKanjiArr[index],
                IsLast = _testLength == _currentTestQuestion
            };
            return kanjiStruct;
        }

        public void Destroy()
        {
            _testedKanjiNumbers.Clear();
        }
    }
}
