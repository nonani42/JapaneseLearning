using System;
using System.IO;
using Microsoft.VisualBasic;
//using Microsoft.VisualBasic.FileIO;
using UnityEngine;

namespace TestSpace
{
    internal class CSVParser
    {
        private string _path = Path.Combine(Application.streamingAssetsPath, "WordSO.csv");

        public void ParseCSV()
        {
            //using (TextFieldParser parser = new TextFieldParser(_path))
            //{
            //    parser.TextFieldType = FieldType.Delimited;
            //    parser.SetDelimiters(",");
            //    while (!parser.EndOfData)
            //    {
            //        string[] fields = parser.ReadFields();
            //        //Processing row
            //        var tempSO = ScriptableObject.CreateInstance<WordSO>();
            //        tempSO.name = fields[0] + "WordSO";
            //        tempSO.JpReading = fields[0];
            //        tempSO.Level = (LevelEnum)Int32.Parse(fields[1]);
            //        tempSO.KanaReading = fields[2];
            //        tempSO.TranslationEng = fields[3];
            //        tempSO.TranslationRus = fields[3];
            //        foreach (string field in fields)
            //        {
            //            //TODO: Process field
            //        }
            //    }
            //}
        }
    }
}
