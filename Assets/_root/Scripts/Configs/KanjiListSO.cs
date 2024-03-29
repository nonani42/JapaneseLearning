using UnityEngine;

[CreateAssetMenu(fileName = nameof(KanjiListSO), menuName = "Configs/" + nameof(KanjiListSO))]
public class KanjiListSO : ScriptableObject
{
    [field: SerializeField] public KanjiSO[] KanjiList { get; private set; }
}
