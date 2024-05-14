using System.Collections.Generic;

namespace TestSpace
{
    internal interface ILoadSaveModel
    {
        void Init(LoginController loginController);
        List<char> LoadKnownKanji();
        void SaveKnownKanji(List<char> knownKanjiList);
        (int oralQuestionsNum, int writingQuestionsNum) LoadQuestionsNumber();
        void SaveQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum);
    }
}
