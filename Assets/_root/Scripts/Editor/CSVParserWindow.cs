using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyEditor
{
    public class CSVParserWindow : EditorWindow
    {
        private string _readKanjiPath = "Assets/_root/CSVs";
        private string _kanjiCSVFileName = "KanjiCSV.csv";
        private string _saveKanjiPath = "Assets/_root/ScriptableObjects/Kanji";

        private string _readWordsPath = "Assets/_root/CSVs";
        private string _wordsCSVFileName = "WordsCSV.csv";
        private string _saveWordsPath = "Assets/_root/ScriptableObjects/Words";

        private string _readKanaPath = "Assets/_root/CSVs";
        private string _kanaCSVFileName = "KanaCSV.csv";
        private string _saveKanaPath = "Assets/_root/ScriptableObjects/Kana";

        private string _readKeyPath = "Assets/_root/CSVs";
        private string _keyCSVFileName = "RadicalsCSV.csv";
        private string _saveKeyPath = "Assets/_root/ScriptableObjects/Keys";

        private string _strokeOrderPath = "Assets/_root/Sprites/StrokeOrder/single";

        private CSVParser _csvParser;

        public CSVParser CsvParser
        {
            get
            {
                if (_csvParser == null)
                    _csvParser = new CSVParser();
                return _csvParser;
            }
            set => _csvParser = value;
        }


        [MenuItem("Custom/CSVParser")]
        public static void ShowWindow()
        {
            var window = GetWindow<CSVParserWindow>();
            window.titleContent = new GUIContent(typeof(CSVParserWindow).ToString());
            window.minSize = new Vector2(800, 600);
        }

        public void OnGUI()
        {
            EditorGUILayout.HelpBox("Type CSV UTF-8. Divider ,", MessageType.Info);

            SetLabel("Kanji");

            ComposeKanji();

            SetLabel("Words");

            ComposeWords();

            SetLabel("Kana");

            ComposeKana();

            SetLabel("Keys");

            ComposeKeys();
        }

        private void ComposeKanji()
        {
            if (GUILayout.Button("Clear Kanji SO"))
                CsvParser.ClearKanjiSO();

            _readKanjiPath = EditorGUILayout.TextField("WordsCSVPath", _readKanjiPath);
            _kanjiCSVFileName = EditorGUILayout.TextField("KanjiCSVFileName", _kanjiCSVFileName);
            _saveKanjiPath = EditorGUILayout.TextField("SaveWordsPath", _saveKanjiPath);

            if (GUILayout.Button("Parse Kanji"))
            {
                if (!CheckForEmptyString(_readKanjiPath, _saveKanjiPath, _kanjiCSVFileName))
                    CsvParser.ParseKanjiCSV(_readKanjiPath, _kanjiCSVFileName, _saveKanjiPath);
            }

            _strokeOrderPath = EditorGUILayout.TextField("Stroke Order Folder", _strokeOrderPath);

            if (GUILayout.Button("Load Stroke Order in the Kanji Card"))
            {
                CsvParser.LoadKanjiStrokeOrder(_strokeOrderPath);
            }

            if (GUILayout.Button("Load Word Examples in the Kanji Card"))
            {
                CsvParser.LoadKanjiWords();
            }
        }

        private void ComposeWords()
        {
            if (GUILayout.Button("Clear Words SO"))
                CsvParser.ClearWordSO();

            _readWordsPath = EditorGUILayout.TextField("WordsCSVPath", _readWordsPath);
            _wordsCSVFileName = EditorGUILayout.TextField("WordsCSVFileName", _wordsCSVFileName);
            _saveWordsPath = EditorGUILayout.TextField("SaveWordsPath", _saveWordsPath);

            if (GUILayout.Button("Parse Words"))
            {
                if (!CheckForEmptyString(_readWordsPath, _saveWordsPath, _wordsCSVFileName))
                    CsvParser.ParseWordCSV(_readWordsPath, _wordsCSVFileName, _saveWordsPath);
            }
        }

        private void ComposeKana()
        {
            if (GUILayout.Button("Clear Kana SO"))
                CsvParser.ClearKanaSO();

            _readKanaPath = EditorGUILayout.TextField("KanaCSVPath", _readKanaPath);
            _kanaCSVFileName = EditorGUILayout.TextField("KanaCSVFileName", _kanaCSVFileName);
            _saveKanaPath = EditorGUILayout.TextField("SaveKanaPath", _saveKanaPath);

            if (GUILayout.Button("Parse Kana"))
            {
                if (!CheckForEmptyString(_readKanaPath, _saveKanaPath, _kanaCSVFileName))
                    CsvParser.ParseKanaCSV(_readKanaPath, _kanaCSVFileName, _saveKanaPath);
            }
        }

        private void ComposeKeys()
        {
            if (GUILayout.Button("Clear Keys SO"))
                CsvParser.ClearKeySO();

            _readKeyPath = EditorGUILayout.TextField("KeyCSVPath", _readKeyPath);
            _keyCSVFileName = EditorGUILayout.TextField("KeyCSVFileName", _keyCSVFileName);
            _saveKeyPath = EditorGUILayout.TextField("SaveKeyPath", _saveKeyPath);

            if (GUILayout.Button("Parse Keys"))
            {
                if (!CheckForEmptyString(_readKeyPath, _saveKeyPath, _keyCSVFileName))
                    CsvParser.ParseKeyCSV(_readKeyPath, _keyCSVFileName, _saveKeyPath);
            }
        }

        private static void SetLabel(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private bool CheckForEmptyString(string read, string savePath, string file)
        {
            bool res = false;
            if (read == string.Empty || savePath == string.Empty)
            {
                Debug.Log("Path is null");
                res = true;
            }

            if (file == string.Empty)
            {
                Debug.Log("File name is null");
                res = true;
            }

            return res;
        }
    }
}
#endif