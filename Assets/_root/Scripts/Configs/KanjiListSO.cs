using UnityEngine;
namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KanjiListSO), menuName = "Configs/" + nameof(KanjiListSO))]
    public class KanjiListSO : ScriptableObject
    {
        [field: SerializeField] public KanjiCardSO[] KanjiList { get; set; }
    }
}