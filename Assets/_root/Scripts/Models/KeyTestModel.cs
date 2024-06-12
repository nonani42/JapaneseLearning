using System;
using System.Collections.Generic;

namespace TestSpace
{
    public struct TestKeyStruct
    {
        public KeySO Key;
        public bool IsLast;
    }

    internal class KeyTestModel
    {
        private KeySO[] _allKeyArr;
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedNumbers = new List<int>();
        private Random _random = new Random();

        public KeyTestModel(KeySO[] allKanaArr)
        {
            _allKeyArr = allKanaArr;
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

        public TestKeyStruct NextKey() => GetNextKey(_allKeyArr[GetRandomIndex(_allKeyArr.Length)]);

        private TestKeyStruct GetNextKey(KeySO keyName)
        {
            TestKeyStruct keyStruct = new TestKeyStruct()
            {
                Key = keyName,
                IsLast = _testLength == _currentTestQuestion
            };
            return keyStruct;
        }

        public void Destroy()
        {
            _testedNumbers.Clear();
        }
    }

}
