using UnityEditor;
using UnityEngine;
using TestSpace;

public class CSVParserWindow : EditorWindow
{
    private string _readKanjiPath = "Assets/_root/CSVs";
    private string _kanjiCSVFileName = "KanjiCSV.csv";
    private string _saveKanjiPath = "Assets/_root/ScriptableObjects/Kanji";

    private string _readWordsPath = "Assets/_root/CSVs";
    private string _wordsCSVFileName = "WordsCSV.csv";
    private string _saveWordsPath = "Assets/_root/ScriptableObjects/Words";

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
        EditorGUILayout.HelpBox("Divider in CSV must be ;", MessageType.Info);

        SetLabel("Kanji");

        _readKanjiPath = EditorGUILayout.TextField("WordsCSVPath", _readKanjiPath);
        _kanjiCSVFileName = EditorGUILayout.TextField("KanjiCSVFileName", _kanjiCSVFileName);
        _saveKanjiPath = EditorGUILayout.TextField("SaveWordsPath", _saveKanjiPath);

        if (GUILayout.Button("Parse Kanji"))
        {
            if (!CheckForEmptyString(_readKanjiPath, _saveKanjiPath, _kanjiCSVFileName))
                CsvParser.ParseKanjiCSV(_readKanjiPath, _kanjiCSVFileName, _saveKanjiPath);
        }

        SetLabel("Words");

        _readWordsPath = EditorGUILayout.TextField("WordsCSVPath", _readWordsPath);
        _wordsCSVFileName = EditorGUILayout.TextField("WordsCSVFileName", _wordsCSVFileName);
        _saveWordsPath = EditorGUILayout.TextField("SaveWordsPath", _saveWordsPath);

        if (GUILayout.Button("Parse Words"))
        {
            if(!CheckForEmptyString(_readWordsPath, _saveWordsPath, _wordsCSVFileName))
                CsvParser.ParseWordCSV(_readWordsPath, _wordsCSVFileName, _saveWordsPath);
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