using UnityEngine;

namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KanaListSO), menuName = "Configs/" + nameof(KanaListSO))]
    public class KanaListSO : ScriptableObject
    {
        [field: SerializeField] public KanaSO[] KanaList { get; set; }
    }
}
