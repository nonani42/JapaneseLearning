using System;
using System.Collections.Generic;

namespace TestSpace
{
    internal class LoadSaveController : IController
    {
        private ILoadSaveModel _loadSaveModel = new CloudLocalSaveModel();

        private List<char> _knownKanjiList = new();
        private List<string> _knownWordsList = new();
        private (int oralQuestionsNum, int writingQuestionsNum) _kanjiQuestionsNum;
        private (int oralQuestionsNum, int writingQuestionsNum) _wordQuestionsNum;
        private int _kanaQuestionsNum;
        private int _keyQuestionsNum;

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

        public List<string> KnownWordsList
        {
            get => _knownWordsList; 
            private set
            {
                _knownWordsList = value;
                _loadSaveModel.SaveKnownWords(_knownWordsList);
                OnKnownWordsChange?.Invoke(KnownWordsList);
            }
        }

        public (int oralQuestionsNum, int writingQuestionsNum) KanjiQuestionsNum
        {
            get => _kanjiQuestionsNum; 
            private set
            {
                if (_kanjiQuestionsNum.oralQuestionsNum != value.oralQuestionsNum)
                    OnKanjiOralQuestionsChange?.Invoke(value.oralQuestionsNum);

                if (_kanjiQuestionsNum.writingQuestionsNum != value.writingQuestionsNum)
                    OnKanjiWritingQuestionsChange?.Invoke(value.writingQuestionsNum);

                _loadSaveModel.SaveKanjiQuestionsNumber(value.oralQuestionsNum, value.writingQuestionsNum);
                _kanjiQuestionsNum = value;
            }
        }

        public (int oralQuestionsNum, int writingQuestionsNum) WordQuestionsNum
        {
            get => _wordQuestionsNum; 
            private set
            {
                if (_wordQuestionsNum.oralQuestionsNum != value.oralQuestionsNum)
                    OnKanjiOralQuestionsChange?.Invoke(value.oralQuestionsNum);

                if (_wordQuestionsNum.writingQuestionsNum != value.writingQuestionsNum)
                    OnKanjiWritingQuestionsChange?.Invoke(value.writingQuestionsNum);

                _loadSaveModel.SaveWordQuestionsNumber(value.oralQuestionsNum, value.writingQuestionsNum);
                _wordQuestionsNum = value;
            }
        }

        public int KanaQuestionsNum
        {
            get => _kanaQuestionsNum;
            private set
            {
                _kanaQuestionsNum = value;
                _loadSaveModel.SaveKanaQuestions(value);
                OnKanaQuestionsChange?.Invoke(value);
            }
        }
        public int KeyQuestionsNum
        {
            get => _keyQuestionsNum;
            private set
            {
                _keyQuestionsNum = value;
                _loadSaveModel.SaveKeyQuestions(value);
                OnKeyQuestionsChange?.Invoke(value);
            }
        }

        public event Action<List<char>> OnKnownKanjiChange;
        public event Action<List<string>> OnKnownWordsChange;
        public event Action<int> OnKanjiOralQuestionsChange;
        public event Action<int> OnKanjiWritingQuestionsChange;
        public event Action<int> OnWordOralQuestionsChange;
        public event Action<int> OnWordWritingQuestionsChange;
        public event Action<int> OnKanaQuestionsChange;
        public event Action<int> OnKeyQuestionsChange;

        public LoadSaveController(LoginController loginController)
        {
            LoadInitialData(loginController);
        }

        private void LoadInitialData(LoginController loginController)
        {
            _loadSaveModel.Init(loginController);
            KnownKanjiList = _loadSaveModel.LoadKnownKanji();
            KnownWordsList = _loadSaveModel.LoadKnownWords();
            KanjiQuestionsNum = _loadSaveModel.LoadKanjiQuestionsNumber();
            WordQuestionsNum = _loadSaveModel.LoadWordQuestionsNumber();
            KanaQuestionsNum = _loadSaveModel.LoadKanaQuestions();
            KeyQuestionsNum = _loadSaveModel.LoadKeyQuestions();
        }

        public void UpdateKnownKanji(List<char> kanjiList) => KnownKanjiList = kanjiList;

        public void UpdateKnownWords(List<string> wordsList) => KnownWordsList = wordsList;

        public void UpdateKanaQuestions(int kanaQuestions) => KanaQuestionsNum = kanaQuestions;

        public void UpdateKeyQuestions(int keyQuestions) => KeyQuestionsNum = keyQuestions;

        public void UpdateKanjiOralQuestionsNum(int oralQuestions)
        {
            _kanjiQuestionsNum.oralQuestionsNum = oralQuestions;
            _loadSaveModel.SaveKanjiQuestionsNumber(oralQuestions, _kanjiQuestionsNum.writingQuestionsNum);
            OnKanjiOralQuestionsChange?.Invoke(oralQuestions);
        }

        public void UpdateKanjiWritingQuestionsNum(int writingQuestions)
        {
            _kanjiQuestionsNum.writingQuestionsNum = writingQuestions;
            _loadSaveModel.SaveKanjiQuestionsNumber(_kanjiQuestionsNum.oralQuestionsNum, writingQuestions);
            OnKanjiWritingQuestionsChange?.Invoke(writingQuestions);
        }

        public void UpdateWordOralQuestionsNum(int oralQuestions)
        {
            _wordQuestionsNum.oralQuestionsNum = oralQuestions;
            _loadSaveModel.SaveKanjiQuestionsNumber(oralQuestions, _wordQuestionsNum.writingQuestionsNum);
            OnWordOralQuestionsChange?.Invoke(oralQuestions);
        }

        public void UpdateWordWritingQuestionsNum(int writingQuestions)
        {
            _wordQuestionsNum.writingQuestionsNum = writingQuestions;
            _loadSaveModel.SaveKanjiQuestionsNumber(_wordQuestionsNum.oralQuestionsNum, writingQuestions);
            OnWordWritingQuestionsChange?.Invoke(writingQuestions);
        }

        public void Destroy()
        {
            _loadSaveModel.SaveKnownKanji(_knownKanjiList);
            _loadSaveModel.SaveKanjiQuestionsNumber(_kanjiQuestionsNum.oralQuestionsNum, _kanjiQuestionsNum.writingQuestionsNum);
            _loadSaveModel.SaveWordQuestionsNumber(_wordQuestionsNum.oralQuestionsNum, _wordQuestionsNum.writingQuestionsNum);
            _loadSaveModel.SaveKanaQuestions(KanaQuestionsNum);
            _loadSaveModel.SaveKeyQuestions(KeyQuestionsNum);
        }
    }
}
