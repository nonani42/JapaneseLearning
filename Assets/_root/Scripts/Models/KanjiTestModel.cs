using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace TestSpace
{
    public struct TestKanjiStruct
    {
        public KanjiCardSO Kanji;
        public bool IsLast;
        public bool IsRepeat;
    }

    internal class KanjiTestModel
    {
        private KanjiCardSO[] _allKanjiArr;
        private List<char> _knownKanjiList = new();
        private List<string> _repeatKanjiList = new();
        private List<string> _currentRepeatKanjiList = new();
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedKanjiNumbers = new List<int>();
        private Random _random = new Random();

        public KanjiTestModel(KanjiCardSO[] allKanjiArr, List<char> knownKanjiList, List<string> repeatKanjiList)
        {
            _allKanjiArr = allKanjiArr;
            _knownKanjiList = knownKanjiList;
            _repeatKanjiList = repeatKanjiList;
            _currentRepeatKanjiList = _repeatKanjiList.ToArray().ToList();
        }

        public void Init()
        {
            _testedKanjiNumbers.Clear();
            _currentTestQuestion = 0;
            _currentRepeatKanjiList = _repeatKanjiList.ToArray().ToList();
        }

        public void SetTestLength(int testLength) => _testLength = testLength;
        public void SetKnownKanji(List<char> knownKanjiList) => _knownKanjiList = knownKanjiList;

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

        private TestKanjiStruct SelectIndex()
        {
            if (_repeatKanjiList.Count == 0)
                return GetNextKanji(_knownKanjiList[GetRandomIndex()]);

            string word;
            int newWordNum;

            for (int i = _currentRepeatKanjiList.Count - 1; i >= 0; i--)
            {
                word = _currentRepeatKanjiList[i];
                if (_knownKanjiList.Contains(word.ToCharArray()[0]))
                {
                    _currentRepeatKanjiList.Remove(word);
                    newWordNum = _knownKanjiList.IndexOf(word.ToCharArray()[0]);
                    _testedKanjiNumbers.Add(newWordNum);
                    return GetNextKanji(word.ToCharArray()[0], true);
                }
                else
                {
                    _repeatKanjiList.Remove(word);
                }
            }

            return GetNextKanji(_knownKanjiList[GetRandomIndex()]);
        }

        public TestKanjiStruct NextKanji() => SelectIndex();

        private TestKanjiStruct GetNextKanji(char kanjiName, bool isRepeat = false)
        {
            int index = _allKanjiArr
                .Select((kanji, index) => new { kanji, index })
                .Where(k => k.kanji.Kanji == kanjiName)
                .FirstOrDefault()
                .index;

            TestKanjiStruct kanjiStruct = new TestKanjiStruct()
            {
                Kanji = _allKanjiArr[index],
                IsLast = _testLength == _currentTestQuestion,
                IsRepeat = isRepeat,
            };
            return kanjiStruct;
        }

        public void Destroy()
        {
            _testedKanjiNumbers.Clear();
        }
    }
}
