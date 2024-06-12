using UnityEngine;
namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KeySO), menuName = "Configs/" + nameof(KeySO))]
    public class KeySO : ScriptableObject
    {
        [field: SerializeField] public int Num { get; set; }
        [field: SerializeField] public string KeyName { get; set; }
        [field: SerializeField] public string KeyVersions { get; set; }
        [field: SerializeField] public int Strokes { get; set; }
        [field: SerializeField] public string Reading { get; set; }
        [field: SerializeField] public string TranslationEng { get; set; }
        [field: SerializeField] public string TranslationRus { get; set; }
        [field: SerializeField] public string ExampleKanji { get; set; }
        [field: SerializeField] public bool IsImportant { get; set; }
    }
}