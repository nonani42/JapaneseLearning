using System.Collections.Generic;
using System.Linq;
using TestSpace;
using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    [CustomEditor(typeof(KeysListSO))]

    internal class KeyListSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Add All Keys SO"))
                AddAllKeys();

            DrawDefaultInspector();
        }

        private void AddAllKeys()
        {
            List<KeySO> keys = AssetDatabase.FindAssets($"t:{typeof(KeySO)}")
                        .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
                        .Cast<KeySO>().ToList();

            (target as KeysListSO).KeyList = keys.ToArray();
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
