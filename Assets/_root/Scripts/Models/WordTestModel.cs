using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSpace
{
    public struct TestWordStruct
    {
        public WordSO Word;
        public bool IsLast;
    }

    internal class WordTestModel
    {
        private WordSO[] _allWordArr;
        private List<string> _knownWordsList = new();
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedWordNumbers = new List<int>();
        private Random _random = new Random();

        public WordTestModel(WordSO[] allWordsList, List<string> knownWordsList)
        {
            _allWordArr = allWordsList;
            _knownWordsList = knownWordsList;
        }

        public void Init()
        {
            _testedWordNumbers.Clear();
            _currentTestQuestion = 0;
        }

        public void SetTestLength(int testLength) => _testLength = testLength;
        public void SetKnownWord(List<string> knownWordsList) => _knownWordsList = knownWordsList;

        private int GetRandomIndex()
        {
            int newWordNum;

            do
            {
                newWordNum = _random.Next(_knownWordsList.Count);
            } while (_testedWordNumbers.Contains(newWordNum));

            _testedWordNumbers.Add(newWordNum);
            _currentTestQuestion++;
            return newWordNum;
        }

        public TestWordStruct NextWord() => GetNextWord(_knownWordsList[GetRandomIndex()]);

        private TestWordStruct GetNextWord(string wordName)
        {
            int index = _allWordArr.Select((word, index) => new { word = word, index }).Where(k => k.word.JpReading == wordName).FirstOrDefault().index;
            TestWordStruct wordStruct = new TestWordStruct()
            {
                Word = _allWordArr[index],
                IsLast = _testLength == _currentTestQuestion
            };
            return wordStruct;
        }

        public void Destroy()
        {
            _testedWordNumbers.Clear();
        }
    }
}
