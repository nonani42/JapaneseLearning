using System.Collections.Generic;
using System.Linq;
using TestSpace;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KanjiListSO))]
public class KanjiListSOEditor : Editor
{
    private string _kanjiToAdd = "日月木山川田人口車門火水金土子女学生先私一二三四五六七八九十百千万円年上下中大小本半分力何明休体好男林森間畑岩目耳手足雨竹米貝石糸花茶肉文字物牛馬鳥魚新古長短高安低暗多少行来帰食飲見聞読書話買教朝昼夜";

    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("Add All Kanji SO"))
            AddAllKanji();

        _kanjiToAdd = EditorGUILayout.TextField("Kanji to Add", _kanjiToAdd);

        if (GUILayout.Button("Add SOME Kanji SO"))
            AddSomeKanji(_kanjiToAdd);

        DrawDefaultInspector();
    }

    private void AddSomeKanji(string kanjiToAdd)
    {
        List<KanjiCardSO> kanji = AssetDatabase.FindAssets($"t:{typeof(KanjiCardSO)}")
                    .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                    .Cast<KanjiCardSO>().ToList();

        char[] kanjiArr = kanjiToAdd.ToCharArray();

        (target as KanjiListSO).KanjiList = kanji.Where(k => kanjiArr.Contains(k.Kanji)).Select(a => a).ToArray();
        SaveAsset(target);
    }

    private void AddAllKanji()
    {
        List<KanjiCardSO> kanji = AssetDatabase.FindAssets($"t:{typeof(KanjiCardSO)}")
                    .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                    .Cast<KanjiCardSO>().ToList();

        (target as KanjiListSO).KanjiList = kanji.ToArray();
        SaveAsset(target);
    }

    private void SaveAsset(Object asset)
    {
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
