using System.Collections.Generic;
using System.Linq;
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
        private KanjiSO[] _allKanjiArr;
        private List<string> _knownKanjiList = new List<string>();
        private int _testLength;
        private int _currentTestQuestion;
        private List<int> _testedKanjiNumbers = new List<int>();
        private Random _random = new Random();

        public KanjiToReadingController(KanjiToReadingPanelView kanjiToReadingPanelView, KanjiSO[] kanjiArr, List<string> knownKanjiList)
        {
            _kanjiToReadingPanelView = kanjiToReadingPanelView;
            _knownKanjiList = knownKanjiList;
            _kanjiToReadingPanelView.OnNextKanji += NextKanji;
            _allKanjiArr = kanjiArr;
        }

        public void Init()
        {
            _testedKanjiNumbers.Clear();
            _currentTestQuestion = 0;
            _kanjiToReadingPanelView.Init();
            _kanjiToReadingPanelView.GetNextKanji();
        }

        public void SetTestLength(int testLength) => _testLength = testLength;
        public void SetKnownKanji(List<string> knownKanjiList) => _knownKanjiList = knownKanjiList;

        private KanjiToReadingStruct NextKanji()
        {
            int newKanjiNum;

            do
            {
                newKanjiNum = _random.Next(_knownKanjiList.Count);
            } while (_testedKanjiNumbers.Contains(newKanjiNum));

            _testedKanjiNumbers.Add(newKanjiNum);
            _currentTestQuestion++;
            return GetNextKanji(_knownKanjiList[newKanjiNum]);
        }

        private KanjiToReadingStruct GetNextKanji(string kanjiName)
        {
            int index = _allKanjiArr.Select((kanji, index)=> new { kanji, index }).Where(k => k.kanji.Kanji == kanjiName).FirstOrDefault().index;
            KanjiToReadingStruct kanji = new KanjiToReadingStruct()
            {
                KanjiText = _allKanjiArr[index].Kanji,
                UpperReadingText = _allKanjiArr[index].UpperReading,
                LowerReadingText = _allKanjiArr[index].LowerReading,
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
