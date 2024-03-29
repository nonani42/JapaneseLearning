using System.Collections.Generic;
using Random = System.Random;

namespace TestSpace
{
    public struct KanjiToReadingStruct
    {
        public string KanjiText;
        public string UpperReadingText;
        public string LowerReadingText;
        public bool IsLast;
    }

    public class KanjiToReadingController : IController
    {
        private KanjiToReadingPanelView _kanjiToReadingPanelView;
        private KanjiSO[] _kanjiArr;
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedKanjiNumbers = new List<int>();
        private Random _random = new Random();

        public KanjiToReadingController(KanjiToReadingPanelView kanjiToReadingPanelView, KanjiSO[] kanjiArr)
        {
            _kanjiToReadingPanelView = kanjiToReadingPanelView;
            _kanjiToReadingPanelView.OnNextKanji += NextKanji;
            _kanjiArr = kanjiArr;
        }

        public void Init()
        {
            _testedKanjiNumbers.Clear();
            _currentTestQuestion = 0;
            _kanjiToReadingPanelView.Init();
            _kanjiToReadingPanelView.GetNextKanji();
        }

        public void SetTestLength(int testLength) => _testLength = testLength;

        private KanjiToReadingStruct NextKanji()
        {
            int newKanjiNum;
            do
            {
                newKanjiNum = _random.Next(_kanjiArr.Length);
            } while (_testedKanjiNumbers.Contains(newKanjiNum));

            if (_kanjiArr.Length >= _testLength)
                _testedKanjiNumbers.Add(newKanjiNum);

            _currentTestQuestion++;
            return GetNextKanji(newKanjiNum);
        }

        private KanjiToReadingStruct GetNextKanji(int index)
        {
            KanjiToReadingStruct kanji = new KanjiToReadingStruct()
            {
                KanjiText = _kanjiArr[index].Kanji,
                UpperReadingText = _kanjiArr[index].UpperReading,
                LowerReadingText = _kanjiArr[index].LowerReading,
                IsLast = _testLength == _currentTestQuestion
            };
            return kanji;
        }

        public void Destroy()
        {
            _kanjiToReadingPanelView.OnNextKanji -= NextKanji;
            _testedKanjiNumbers.Clear();
        }
    }
}
