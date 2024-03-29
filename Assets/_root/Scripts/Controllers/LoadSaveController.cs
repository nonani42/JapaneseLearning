using System.Collections.Generic;

namespace TestSpace
{
    internal class LoadSaveController : IController
    {
        private LoadSaveModel _loadSaveModel = new();

        private List<string> _knownKanjiList = new();
        private (int oralQuestionsNum, int writingQuestionsNum) _questionsNum;

        public List<string> KnownKanjiList { get => _knownKanjiList; private set => _knownKanjiList = value; }
        public (int oralQuestionsNum, int writingQuestionsNum) QuestionsNum { get => _questionsNum; private set => _questionsNum = value; }


        public LoadSaveController()
        {
            _knownKanjiList = _loadSaveModel.LoadKnownKanji();
            _questionsNum = _loadSaveModel.LoadQuestionsNumber();
        }

        public void UpdateKnownKanji(List<string> kanjiList) => _knownKanjiList = kanjiList;

        public void UpdateOralQuestionsNum(int oralQuestions) => _questionsNum.oralQuestionsNum = oralQuestions;

        public void UpdateWritingQuestionsNum(int writingQuestions) => _questionsNum.writingQuestionsNum = writingQuestions;

        public void Destroy()
        {
            _loadSaveModel.SaveKnownKanji(_knownKanjiList);
            _loadSaveModel.SaveQuestionsNumber(_questionsNum.oralQuestionsNum, _questionsNum.writingQuestionsNum);
        }
    }
}
