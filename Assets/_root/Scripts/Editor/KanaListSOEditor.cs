using System.Collections.Generic;
using System.Linq;
using TestSpace;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace MyEditor
{
    [CustomEditor(typeof(KanaListSO))]
    internal class KanaListSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Add All Kana SO"))
                AddAllKana();

            DrawDefaultInspector();
        }

        private void AddAllKana()
        {
            List<KanaSO> kanji = AssetDatabase.FindAssets($"t:{typeof(KanaSO)}")
                        .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                        .Cast<KanaSO>().ToList();

            (target as KanaListSO).KanaList = kanji.ToArray();
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