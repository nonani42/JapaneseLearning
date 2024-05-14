using UnityEditor;
using UnityEngine;

namespace MyEditor
{
    internal class ClearSavedKeys : EditorWindow
    {
        [MenuItem("Custom/ClearSavedKeys")]
        public static void Clear()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs deleted");
        }
    }
}
