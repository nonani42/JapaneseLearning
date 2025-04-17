using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace TestSpace
{
    public struct TestWordStruct
    {
        public WordSO Word;
        public bool IsLast;
        public bool IsRepeat;
    }

    internal class WordTestModel
    {
        private WordSO[] _allWordArr;
        private List<string> _knownWordsList = new();
        private List<string> _repeatWordsList = new();
        private List<string> _currentRepeatWordList = new();
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedWordNumbers = new List<int>();
        private Random _random = new Random();

        public WordTestModel(WordSO[] allWordsArr, List<string> knownWordsList, List<string> repeatWordsList)
        {
            _allWordArr = allWordsArr;
            _knownWordsList = knownWordsList;
            _repeatWordsList = repeatWordsList;
            _currentRepeatWordList = _repeatWordsList.ToArray().ToList();
        }

        public void Init()
        {
            _testedWordNumbers.Clear();
            _currentTestQuestion = 0;
            _currentRepeatWordList = _repeatWordsList.ToArray().ToList();
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

        private TestWordStruct SelectIndex()
        {
            if (_repeatWordsList.Count == 0)
                return GetNextWord(_knownWordsList[GetRandomIndex()]);

            string word;
            int newWordNum;

            for (int i = _currentRepeatWordList.Count - 1; i >= 0; i--)
            {
                word = _currentRepeatWordList[i];
                if (_knownWordsList.Contains(word))
                {
                    _currentRepeatWordList.Remove(word);
                    newWordNum = _knownWordsList.IndexOf(word);
                    _testedWordNumbers.Add(newWordNum);
                    return GetNextWord(word, true);
                }
                else
                {
                    _repeatWordsList.Remove(word);
                }
            }

            return GetNextWord(_knownWordsList[GetRandomIndex()]);
        }

        public TestWordStruct NextWord() => SelectIndex();

        private TestWordStruct GetNextWord(string wordName, bool isRepeat = false)
        {
            int index = _allWordArr
                .Select((word, index) => new { word, index })
                .Where(k => k.word.JpReading == wordName)
                .FirstOrDefault()
                .index;

            TestWordStruct wordStruct = new TestWordStruct()
            {
                Word = _allWordArr[index],
                IsLast = _testLength == _currentTestQuestion,
                IsRepeat = isRepeat,
            };
            return wordStruct;
        }

        public void Destroy()
        {
            _testedWordNumbers.Clear();
        }
    }
}
