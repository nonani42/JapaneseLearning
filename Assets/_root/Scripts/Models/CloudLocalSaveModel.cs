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
        private const string ORAL_QUESTIONS = "oralQuestionsNum";
        private const string WRITTEN_QUESTIONS = "writtenQuestionsNum";

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

        public (int oralQuestionsNum, int writingQuestionsNum) LoadQuestionsNumber()
        {
            int oral = 0;
            int written = 0;

            if (_result == null)
                return (oral, written);

            if (_result.Data.ContainsKey(ORAL_QUESTIONS))
                oral = int.Parse(_result.Data[ORAL_QUESTIONS].Value);

            if (_result.Data.ContainsKey(WRITTEN_QUESTIONS))
                written = int.Parse(_result.Data[WRITTEN_QUESTIONS].Value);

            return (oral, written);
        }

        public void SaveKnownKanji(List<char> knownKanjiList)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < knownKanjiList.Count; i++)
            {
                sb.Append(knownKanjiList[i]);
                sb.Append(SEPARATOR);
            }

            SaveKanji(sb.ToString());
        }

        public void SaveQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum) => SaveQuestions(oralQuestionsNum.ToString(), writingQuestionsNum.ToString());

        private void SaveKanji(string knownKanji)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { KNOWN_KANJI, knownKanji },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void SaveQuestions(string oral, string written)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { ORAL_QUESTIONS, oral },
                    { WRITTEN_QUESTIONS, written },
                }
            };

            PlayFabClientAPI.UpdateUserData(request, OnUpdateSuccess, OnUpdateFailure);
        }

        private void OnUpdateSuccess(UpdateUserDataResult result) => Debug.Log("UpdateSuccess");

        private void OnUpdateFailure(PlayFabError error) => Debug.Log($"{error.GenerateErrorReport()}");
    }
}
