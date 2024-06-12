using UnityEngine;

namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KeysListSO), menuName = "Configs/" + nameof(KeysListSO))]
    public class KeysListSO : ScriptableObject
    {
        [field: SerializeField] public KeySO[] KeyList { get; set; }
    }
}
