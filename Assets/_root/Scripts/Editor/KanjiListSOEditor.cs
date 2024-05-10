using System.Collections.Generic;
using System.Linq;
using TestSpace;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KanjiListSO))]
public class KanjiListSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add All Kanji SO"))
        {
            List<KanjiCardSO> kanji = AssetDatabase.FindAssets($"t:{typeof(KanjiCardSO)}")
                        .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                        .Cast<KanjiCardSO>().ToList();

            (target as KanjiListSO).KanjiList = kanji.ToArray();
        }
        DrawDefaultInspector();
    }
}
