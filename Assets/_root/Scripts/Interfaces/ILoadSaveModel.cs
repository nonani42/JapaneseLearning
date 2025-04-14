using System.Collections.Generic;

namespace TestSpace
{
    internal interface ILoadSaveModel
    {
        void Init(LoginController loginController);
        List<char> LoadKnownKanji();
        List<string> LoadKnownWords();
        void SaveKnownKanji(List<char> knownKanjiList);
        void SaveKnownWords(List<string> knownWordsList);
        (int oralQuestionsNum, int writingQuestionsNum) LoadKanjiQuestionsNumber();
        void SaveKanjiQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum);
        (int oralQuestionsNum, int writingQuestionsNum) LoadWordQuestionsNumber();
        void SaveWordQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum);
        int LoadKanaQuestions();
        void SaveKanaQuestions(int kanaQuestionsNum);
        int LoadKeyQuestions();
        void SaveKeyQuestions(int kanaQuestionsNum);
    }
}
