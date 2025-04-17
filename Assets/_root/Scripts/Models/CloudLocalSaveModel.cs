using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TestSpace
{
    internal class CloudLocalSaveModel : ILoadSaveModel
    {
        private const char SEPARATOR = '|'; 
        private const string KNOWN_KANJI = "knownKanjiList";
        private const string KNOWN_WORDS = "knownWordsList";
        private const string REPEAT_KANJI = "repeatKanjiList";
        private const string REPEAT_WORDS = "repeatWordsList";
        private const string ORAL_kANJI_QUESTIONS = "oralQuestionsNum";
        private const string ORAL_WORD_QUESTIONS = "oralWordQuestionsNum";
        private const string WRITTEN_KANJI_QUESTIONS = "writtenQuestionsNum";
        private const string WRITTEN_WORD_QUESTIONS = "writtenWordQuestionsNum";
        private const string KANA_QUESTIONS = "kanaQuestionsNum";
        private const string KEY_QUESTIONS = "keyQuestionsNum";

        private LoginController _loginController;

        private GetUserDataResult _result;

        public void Init(LoginController loginController)
        {
            _loginController = loginController;
            _result = _loginController.Result;
        }

        public List<char> LoadKnownKanji()
        {
            List<char> res = new List<char>();

            if (_result == null)
                return res;

            if (_result.Data.ContainsKey(KNOWN_KANJI))
            {
                string[] arr = _result.Data[KNOWN_KANJI].Value.Split(SEPARATOR);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == string.Empty)
                        continue;
                    res.Add(arr[i].ToCharArray()[0]);
                }
            }
            return res;
        }

        public List<string> LoadKnownWords()
        {
            List<string> res = new List<string>();

            if (_result == null)
                return res;

            if (_result.Data.ContainsKey(KNOWN_WORDS))
            {
                string[] arr = _result.Data[KNOWN_WORDS].Value.Split(SEPARATOR);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == string.Empty)
                        continue;
                    res.Add(arr[i]);
                }
            }
            return res;
        }

        public (int oralQuestionsNum, int writingQuestionsNum) LoadKanjiQuestionsNumber()
        {
            int oral = 0;
            int written = 0;

            if (_result == null)
                return (oral, written);

            if (_result.Data.ContainsKey(ORAL_kANJI_QUESTIONS))
                oral = int.Parse(_result.Data[ORAL_kANJI_QUESTIONS].Value);

            if (_result.Data.ContainsKey(WRITTEN_KANJI_QUESTIONS))
                written = int.Parse(_result.Data[WRITTEN_KANJI_QUESTIONS].Value);

            return (oral, written);
        }

        public (int oralQuestionsNum, int writingQuestionsNum) LoadWordQuestionsNumber()
        {
            int oral = 0;
            int written = 0;

            if (_result == null)
                return (oral, written);

            if (_result.Data.ContainsKey(ORAL_WORD_QUESTIONS))
                oral = int.Parse(_result.Data[ORAL_WORD_QUESTIONS].Value);

            if (_result.Data.ContainsKey(WRITTEN_WORD_QUESTIONS))
                written = int.Parse(_result.Data[WRITTEN_WORD_QUESTIONS].Value);

            return (oral, written);
        }

        public int LoadKanaQuestions() => LoadQuestions(KANA_QUESTIONS);
        public int LoadKeyQuestions() => LoadQuestions(KEY_QUESTIONS);

        private int LoadQuestions(string key)
        {
            int questions = 0;

            if (_result == null)
                return questions;

            if (_result.Data.ContainsKey(key))
                questions = int.Parse(_result.Data[key].Value);

            return questions;
        }

        public void SaveKnownKanji(List<char> knownKanjiList)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < knownKanjiList.Count; i++)
            {
                sb.Append(knownKanjiList[i]);
                sb.Append(SEPARATOR);
            }

            SaveKanji(sb.ToString(), KNOWN_KANJI);
        }

        public void SaveKnownWords(List<string> knownWordsList)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < knownWordsList.Count; i++)
            {
                sb.Append(knownWordsList[i]);
                sb.Append(SEPARATOR);
            }

            SaveWords(sb.ToString(), KNOWN_WORDS);
        }

        public void SaveKanjiQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum) => SaveKanjiQuestions(oralQuestionsNum.ToString(), writingQuestionsNum.ToString());
        public void SaveWordQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum) => SaveWordQuestions(oralQuestionsNum.ToString(), writingQuestionsNum.ToString());
        public void SaveKanaQuestions(int kanaQuestionsNum) => SaveQuestions(kanaQuestionsNum.ToString(), KANA_QUESTIONS);
        public void SaveKeyQuestions(int keyQuestionsNum) => SaveQuestions(keyQuestionsNum.ToString(), KEY_QUESTIONS);

        private void SaveKanji(string knownKanji, string key)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { key, knownKanji },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void SaveWords(string knownWords, string key)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { key, knownWords },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void SaveKanjiQuestions(string oral, string written)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { ORAL_kANJI_QUESTIONS, oral },
                    { WRITTEN_KANJI_QUESTIONS, written },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void SaveWordQuestions(string oral, string written)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { ORAL_WORD_QUESTIONS, oral },
                    { WRITTEN_WORD_QUESTIONS, written },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void SaveQuestions(string questionsNum, string key)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { key, questionsNum },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void OnUpdateSuccess(UpdateUserDataResult result) => Debug.Log("UpdateSuccess");

        private void OnUpdateFailure(PlayFabError error) => Debug.Log($"{error.GenerateErrorReport()}");

        public List<string> LoadRepeatKanji()
        {
            List<string> res = new List<string>();

            if (_result == null)
                return res;

            if (_result.Data.ContainsKey(REPEAT_KANJI))
            {
                string[] arr = _result.Data[REPEAT_KANJI].Value.Split(SEPARATOR);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == string.Empty)
                        continue;
                    res.Add(arr[i]);
                }
            }
            return res;
        }

        public List<string> LoadRepeatWords()
        {
            List<string> res = new List<string>();

            if (_result == null)
                return res;

            if (_result.Data.ContainsKey(REPEAT_WORDS))
            {
                string[] arr = _result.Data[REPEAT_WORDS].Value.Split(SEPARATOR);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == string.Empty)
                        continue;
                    res.Add(arr[i]);
                }
            }
            return res;
        }

        public void SaveRepeatKanji(List<string> repeatKanji)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < repeatKanji.Count; i++)
            {
                sb.Append(repeatKanji[i]);
                sb.Append(SEPARATOR);
            }

            SaveKanji(sb.ToString(), REPEAT_KANJI);
        }

        public void SaveRepeatWords(List<string> repeatWords)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < repeatWords.Count; i++)
            {
                sb.Append(repeatWords[i]);
                sb.Append(SEPARATOR);
            }

            SaveWords(sb.ToString(), REPEAT_WORDS);
        }
    }
}
