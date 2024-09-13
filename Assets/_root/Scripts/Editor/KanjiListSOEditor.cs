using System.Collections.Generic;
using System.Linq;
using TestSpace;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyEditor
{
    [CustomEditor(typeof(KanjiListSO))]
    public class KanjiListSOEditor : Editor
    {
        private string _kanjiToAdd = "日月木山川田人口車門火水金土子女学生先私一二三四五六七八九十百千万円年上下中大小本半分力何明休体好男林森間畑岩目耳手足雨竹米貝石糸花茶肉文字物牛馬鳥魚新古長短高安低暗多少行来帰食飲見聞読書話買教朝昼夜晩夕方午前後毎週曜作泳油海酒待校時言計語飯宅客室家英薬会今雪雲電売広店度病疲痛屋国回困開閉近遠速遅道青晴静寺持荷歌友父母兄姉弟妹夫妻彼主奥元気有名親切便利不若早忙出入乗降着渡通走歩止動働右左東西北南外";

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
}
#endif