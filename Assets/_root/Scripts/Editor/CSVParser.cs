using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using TestSpace;

#if UNITY_EDITOR
namespace MyEditor
{
    public class CSVParser
    {
        private const string WORD_FILE_END = "WordSO";
        private const string KANJI_FILE_END = "KanjiCardSO";
        private const string KANA_FILE_END = "KanaSO";
        private const string KEY_FILE_END = "KeySO";
        private const string STROKE_ORDER_FILE_END = "StrokeOrder";

        public void ClearKeySO()
        {
            List<string> keyList = AssetDatabase.FindAssets($"t:{typeof(KeySO)}")
                                                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                    .ToList();

            foreach (var key in keyList)
                AssetDatabase.DeleteAsset(key);
        }

        public void ClearKanaSO()
        {
            List<string> kanaList = AssetDatabase.FindAssets($"t:{typeof(KanaSO)}")
                                                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                    .ToList();

            foreach (var kana in kanaList)
                AssetDatabase.DeleteAsset(kana);
        }

        public void ClearWordSO()
        {
            List<string> wordList = AssetDatabase.FindAssets($"t:{typeof(WordSO)}")
                                                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                    .ToList();

            foreach (var word in wordList)
                AssetDatabase.DeleteAsset(word);
        }

        public void ClearKanjiSO() 
        {
            List<string> kanjiList = AssetDatabase.FindAssets($"t:{typeof(KanjiCardSO)}")
                                                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                    .ToList();

            foreach (var kanji in kanjiList)
                AssetDatabase.DeleteAsset(kanji);
        }

        public void LoadKanjiStrokeOrder(string strokeOrderFolder) 
        {
            List<string> kanjiPathList = AssetDatabase.FindAssets($"t:{typeof(KanjiCardSO)}")
                                                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                    .ToList();

            foreach (var kanjiPath in kanjiPathList)
            {
                KanjiCardSO currentKanji = AssetDatabase.LoadAssetAtPath<KanjiCardSO>(kanjiPath);
                char currentName = currentKanji.Kanji;
                string[] foldersToSearch = new[] { strokeOrderFolder };
                string strokeOrderPath = AssetDatabase.FindAssets($"{currentName}{STROKE_ORDER_FILE_END}", foldersToSearch)
                                                        .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                        .FirstOrDefault();
                Sprite temp;
                if (strokeOrderPath != null)
                {
                    temp = AssetDatabase.LoadAssetAtPath<Sprite>(strokeOrderPath);
                }
                else
                {
                    temp = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.FindAssets("placeHolderStrokeOrder")
                                                                                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                                                .FirstOrDefault());
                }
                currentKanji.StrokeOrder = temp;
                EditorUtility.SetDirty(currentKanji);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        public void LoadKanjiWords()
        {
            List<string> kanjiPathList = AssetDatabase.FindAssets($"t:{typeof(KanjiCardSO)}")
                                                    .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                    .ToList();

            List<string> wordPathList = AssetDatabase.FindAssets($"t:{typeof(WordSO)}")
                                                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                                                .ToList();

            Dictionary<string, string> wordDic = new Dictionary<string, string>();
            foreach (var path in wordPathList)
            {
                WordSO currentWord = AssetDatabase.LoadAssetAtPath<WordSO>(path);
                wordDic.Add(currentWord.JpReading, path);
            }

            foreach (var path in kanjiPathList)
            {
                KanjiCardSO currentKanjiCard = AssetDatabase.LoadAssetAtPath<KanjiCardSO>(path);
                char currentKanji = currentKanjiCard.Kanji;
                List<WordSO> wordList = new();

                foreach (string word in wordDic.Keys)
                {
                    if (word.Contains(currentKanji))
                        wordList.Add(AssetDatabase.LoadAssetAtPath<WordSO>(wordDic.GetValueOrDefault(word)));
                }
                currentKanjiCard.Examples = wordList.ToArray();
                EditorUtility.SetDirty(currentKanjiCard);
            }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
        }

        public void ParseWordCSV(string readPath, string fileName, string savePath)
        {
            string fileCSV = Path.Combine(readPath, fileName);
            bool isHeader = true;
            using (StreamReader streamReader = new StreamReader(fileCSV))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(";");
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    //Processing row
                    var tempSO = ScriptableObject.CreateInstance<WordSO>();
                    tempSO.name = fields[0] + WORD_FILE_END + ".asset";
                    tempSO.JpReading = fields[0];
                    tempSO.Level = (LevelEnum)Int32.Parse(fields[1]);
                    tempSO.KanaReading = fields[2];
                    tempSO.TranslationEng = fields[3];
                    tempSO.TranslationRus = fields[4];
                    tempSO.SpecialReading = Int32.Parse(fields[5]) != 0;
                    tempSO.KanaOnly = Int32.Parse(fields[6]) != 0;

                    Save(savePath, tempSO);
                }
            }
        }

        public void ParseKanjiCSV(string readPath, string fileName, string savePath)
        {
            string fileCSV = Path.Combine(readPath, fileName);
            bool isHeader = true;
            using (StreamReader streamReader = new StreamReader(fileCSV))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(";");
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    //Processing row
                    var tempSO = ScriptableObject.CreateInstance<KanjiCardSO>();
                    tempSO.name = fields[0] + KANJI_FILE_END + ".asset";
                    tempSO.Kanji = fields[0].ToCharArray()[0];
                    tempSO.Level = (LevelEnum)Int32.Parse(fields[1]);
                    tempSO.Grade = (GradeEnum)Int32.Parse(fields[2]);
                    tempSO.Strokes = Int32.Parse(fields[3]);
                    tempSO.UpperReading = fields[4];
                    tempSO.LowerReading = fields[5];
                    tempSO.MeaningEng = fields[6];
                    tempSO.MeaningRus = fields[7];

                    Save(savePath, tempSO);
                }
            }
        }

        public void ParseKanaCSV(string readPath, string fileName, string savePath)
        {
            string fileCSV = Path.Combine(readPath, fileName);
            bool isHeader = true;
            using (StreamReader streamReader = new StreamReader(fileCSV))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(";");
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    //Processing row
                    var tempSO = ScriptableObject.CreateInstance<KanaSO>();
                    tempSO.name = fields[0] + fields[1] + KANA_FILE_END + ".asset";
                    tempSO.KatakanaKeyName = fields[0];
                    tempSO.HiraganaKeyName = fields[1];
                    tempSO.Reading = fields[2];
                    tempSO.IsTested = Int32.Parse(fields[3]) != 0;

                    Save(savePath, tempSO);
                }
            }
        }

        public void ParseKeyCSV(string readPath, string fileName, string savePath)
        {
            string fileCSV = Path.Combine(readPath, fileName);
            bool isHeader = true;
            using (StreamReader streamReader = new StreamReader(fileCSV))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(";");
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    //Processing row
                    var tempSO = ScriptableObject.CreateInstance<KeySO>();
                    tempSO.name = fields[0] + fields[1] + KEY_FILE_END + ".asset";
                    tempSO.Num = Int32.Parse(fields[0]);
                    tempSO.KeyName = fields[1];
                    tempSO.KeyVersions = fields[2];
                    tempSO.Strokes = Int32.Parse(fields[3]);
                    tempSO.Reading = fields[4];
                    tempSO.TranslationEng = fields[5];
                    tempSO.TranslationRus = fields[6];
                    tempSO.ExampleKanji = fields[7];
                    tempSO.IsImportant = Int32.Parse(fields[8]) != 0;

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
#endif