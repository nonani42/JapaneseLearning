using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSpace
{
    public struct TestKanaStruct
    {
        public KanaSO Kana;
        public bool IsLast;
    }

    internal class KanaTestModel
    {
        private KanaSO[] _allKanaArr;
        private List<KanaSO> _kanaForTestList;
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedNumbers = new List<int>();
        private Random _random = new Random();

        public KanaTestModel(KanaSO[] allKanaArr)
        {
            _allKanaArr = allKanaArr;
            _kanaForTestList = _allKanaArr.Where(x => x.IsTested == true).ToList();
        }

        public void Init()
        {
            _testedNumbers.Clear();
            _currentTestQuestion = 0;
        }

        public void SetTestLength(int testLength) => _testLength = testLength;

        private int GetRandomIndex(int count)
        {
            int newNum;

            do
            {
                newNum = _random.Next(count);
            } while (_testedNumbers.Contains(newNum));

            _testedNumbers.Add(newNum);
            _currentTestQuestion++;
            return newNum;
        }

        public TestKanaStruct NextKana() => GetNextKana(_kanaForTestList[GetRandomIndex(_kanaForTestList.Count)]);

        private TestKanaStruct GetNextKana(KanaSO kanjiName)
        {
            TestKanaStruct kanaStruct = new TestKanaStruct()
            {
                Kana = kanjiName,
                IsLast = _testLength == _currentTestQuestion
            };
            return kanaStruct;
        }

        public void Destroy()
        {
            _testedNumbers.Clear();
        }
    }
}
