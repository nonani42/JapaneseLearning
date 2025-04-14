using UnityEngine;
namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(WordListSO), menuName = "Configs/" + nameof(WordListSO))]
    public class WordListSO : ScriptableObject
    {
        [field: SerializeField] public WordSO[] WordList { get; set; }
    }
}