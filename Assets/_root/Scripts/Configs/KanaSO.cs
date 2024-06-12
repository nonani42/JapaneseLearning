using UnityEngine;

namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KanaSO), menuName = "Configs/" + nameof(KanaSO))]
    public class KanaSO : ScriptableObject
    {
        [field: SerializeField] public string HiraganaKeyName { get; set; }
        [field: SerializeField] public string KatakanaKeyName { get; set; }
        [field: SerializeField] public string Reading { get; set; }
        [field: SerializeField] public bool IsTested { get; set; }
    }
}
