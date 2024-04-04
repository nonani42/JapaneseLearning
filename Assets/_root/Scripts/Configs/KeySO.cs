using UnityEngine;
namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KeySO), menuName = "Configs/" + nameof(KeySO))]
    public class KeySO : ScriptableObject
    {
        [field: SerializeField] public string KeyName { get; private set; }
        [field: SerializeField] public string Reading { get; private set; }
        [field: SerializeField] public string TranslationEng { get; private set; }
        [field: SerializeField] public string TranslationRus { get; private set; }
    }
}