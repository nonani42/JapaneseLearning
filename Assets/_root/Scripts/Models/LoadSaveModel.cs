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

    internal class LoadSaveModel
    {
        private SerializableXMLData<DataToRemember> _serializableXMLKanji = new SerializableXMLData<DataToRemember>();
        private SerializableXMLData<QuestionsToRemember> _serializableXMLParams = new SerializableXMLData<QuestionsToRemember>();
        private string _kanjiPath = Path.Combine(Application.streamingAssetsPath, "KanjiGameSave.xml");
        private string _paramsPath = Path.Combine(Application.streamingAssetsPath, "ParamsSave.xml");

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

        public (int oralQuestionsNum, int writingQuestionsNum) LoadQuestionsNumber()
        {
            QuestionsToRemember _savedData = _serializableXMLParams.Load(_paramsPath);
            
            if (_savedData != null)
                return (_savedData.OralQuestions, _savedData.WritingQuestions);
            else
                return (0, 0);
        }

        public void SaveQuestionsNumber(int oralQuestionsNum, int writingQuestionsNum)
        {
            QuestionsToRemember data = new QuestionsToRemember()
            {
                OralQuestions = oralQuestionsNum,
                WritingQuestions = writingQuestionsNum,
            };

            _serializableXMLParams.Save(data, _paramsPath);
        }
    }
}
