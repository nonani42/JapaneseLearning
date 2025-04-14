using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TestSpace
{
    [Serializable]
    public class DataToRemember
    {
        public List<char> KnownKanji;
    }

    [Serializable]
    public class QuestionsToRemember
    {
        public int OralQuestions;
        public int WritingQuestions;
    }

    internal class LocalLoadSaveModel : ILoadSaveModel
    {
        private SerializableXMLData<DataToRemember> _serializableXMLKanji = new SerializableXMLData<DataToRemember>();
        private SerializableXMLData<QuestionsToRemember> _serializableXMLParams = new SerializableXMLData<QuestionsToRemember>();
        private string _kanjiPath;
        private string _paramsPath;

        private LoginController _loginController;

        public void Init(LoginController loginController)
        {
            _loginController = loginController;

            string kanjiFileName = $"KanjiGameSave{_loginController.Username}.xml";
            string paramsFileName = $"ParamsSave{_loginController.Username}.xml";

            _kanjiPath = Path.Combine(Application.streamingAssetsPath, kanjiFileName);
            _paramsPath = Path.Combine(Application.streamingAssetsPath, paramsFileName);
        }

        public List<char> LoadKnownKanji()
        {
            DataToRemember _savedData = _serializableXMLKanji.Load(_kanjiPath);

            List<char> knownKanjiList = new();

            if (_savedData != null)
            {
                for (int i = 0; i < _savedData.KnownKanji.Count; i++)
                    knownKanjiList.Add(_savedData.KnownKanji[i]);
            }

            return knownKanjiList;
        }

        public void SaveKnownKanji(List<char> knownKanjiList)
        {
            DataToRemember data = new DataToRemember()
            {
                KnownKanji = knownKanjiList,
            };

            _serializableXMLKanji.Save(data, _kanjiPath);
        }

        public (int oralQuestionsNum, int writingQuestionsNum) LoadKanjiQuestionsNumber()
        {
            QuestionsToRemember _savedData = _serializableXMLParams.Load(_paramsPath);
            
            if (_savedData != null)
                return (_savedData.OralQuestions, _savedData.WritingQuestions);
            else
                return (0, 0);
        }

        public void SaveKanjiQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum)
        {
            QuestionsToRemember data = new QuestionsToRemember()
            {
                OralQuestions = oralQuestionsNum,
                WritingQuestions = writingQuestionsNum,
            };

            _serializableXMLParams.Save(data, _paramsPath);
        }

        public int LoadKanaQuestions()
        {
            throw new NotImplementedException();
        }

        public void SaveKanaQuestions(int kanaQuestionsNum)
        {
            throw new NotImplementedException();
        }

        public int LoadKeyQuestions()
        {
            throw new NotImplementedException();
        }

        public void SaveKeyQuestions(int kanaQuestionsNum)
        {
            throw new NotImplementedException();
        }

        public (int oralQuestionsNum, int writingQuestionsNum) LoadWordQuestionsNumber()
        {
            throw new NotImplementedException();
        }

        public void SaveWordQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum)
        {
            throw new NotImplementedException();
        }

        public List<string> LoadKnownWords()
        {
            throw new NotImplementedException();
        }

        public void SaveKnownWords(List<string> knownWordsList)
        {
            throw new NotImplementedException();
        }
    }
}
