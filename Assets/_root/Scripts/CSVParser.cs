using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace TestSpace
{
    public class CSVParser
    {
        public void ParseWordCSV(string readPath, string fileName, string savePath)
        {
            string fileCSV = Path.Combine(readPath, fileName);
            using (StreamReader streamReader = new StreamReader(fileCSV))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(";");
                    //Processing row
                    var tempSO = ScriptableObject.CreateInstance<WordSO>();
                    tempSO.name = fields[0] + "WordSO.asset";
                    tempSO.JpReading = fields[0];
                    tempSO.Level = (LevelEnum)Int32.Parse(fields[1]);
                    tempSO.KanaReading = fields[2];
                    tempSO.TranslationEng = fields[3];
                    tempSO.TranslationRus = fields[4];

                    Save(savePath, tempSO);
                }
            }
        }

        public void ParseKanjiCSV(string readPath, string fileName, string savePath)
        {
            return;
            string fileCSV = Path.Combine(readPath, fileName);
            using (StreamReader streamReader = new StreamReader(fileCSV))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(";");
                    //Processing row
                    var tempSO = ScriptableObject.CreateInstance<WordSO>();
                    tempSO.name = fields[0] + "WordSO.asset";
                    tempSO.JpReading = fields[0];
                    tempSO.Level = (LevelEnum)Int32.Parse(fields[1]);
                    tempSO.KanaReading = fields[2];
                    tempSO.TranslationEng = fields[3];
                    tempSO.TranslationRus = fields[4];
                    //foreach (string field in fields)
                    //{
                    //    //TODO: Process field
                    //}

                    Save(savePath, tempSO);
                }
            }
        }

        private void Save(string path, ScriptableObject so)
        {
            string savePath = Path.Combine(path, so.name);
            AssetDatabase.CreateAsset(so, savePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = so;
        }
    }
}
