using System;
using System.Collections.Generic;

namespace TestSpace
{
    internal class LoadSaveController : IController
    {
        private ILoadSaveModel _loadSaveModel = new CloudLocalSaveModel();

        private List<char> _knownKanjiList = new();
        private (int oralQuestionsNum, int writingQuestionsNum) _questionsNum;

        public List<char> KnownKanjiList
        {
            get => _knownKanjiList; 
            private set
            {
                _knownKanjiList = value;
                _loadSaveModel.SaveKnownKanji(_knownKanjiList);
                OnKnownKanjiChange?.Invoke(KnownKanjiList);
            }
        }
        public (int oralQuestionsNum, int writingQuestionsNum) QuestionsNum
        {
            get => _questionsNum; 
            private set
            {
                if (_questionsNum.oralQuestionsNum != value.oralQuestionsNum)
                    OnOralQuestionsChange?.Invoke(value.oralQuestionsNum);

                if (_questionsNum.writingQuestionsNum != value.writingQuestionsNum)
                    OnWritingQuestionsChange?.Invoke(value.writingQuestionsNum);

                _loadSaveModel.SaveQuestionsNumber(value.oralQuestionsNum, value.writingQuestionsNum);
                _questionsNum = value;
            }
        }

        public event Action<List<char>> OnKnownKanjiChange;
        public event Action<int> OnOralQuestionsChange;
        public event Action<int> OnWritingQuestionsChange;

        public LoadSaveController(LoginController loginController)
        {
            LoadInitialData(loginController);
        }

        private void LoadInitialData(LoginController loginController)
        {
            _loadSaveModel.Init(loginController);
            KnownKanjiList = _loadSaveModel.LoadKnownKanji();
            QuestionsNum = _loadSaveModel.LoadQuestionsNumber();
        }

        public void UpdateKnownKanji(List<char> kanjiList) => KnownKanjiList = kanjiList;

        public void UpdateOralQuestionsNum(int oralQuestions)
        {
            _questionsNum.oralQuestionsNum = oralQuestions;
            OnOralQuestionsChange?.Invoke(oralQuestions);
        }

        public void UpdateWritingQuestionsNum(int writingQuestions)
        {
            _questionsNum.writingQuestionsNum = writingQuestions;
            OnWritingQuestionsChange?.Invoke(writingQuestions);
        }

        public void Destroy()
        {
            //_loadSaveModel.SaveKnownKanji(_knownKanjiList);
            //_loadSaveModel.SaveQuestionsNumber(_questionsNum.oralQuestionsNum, _questionsNum.writingQuestionsNum);
        }
    }
}
