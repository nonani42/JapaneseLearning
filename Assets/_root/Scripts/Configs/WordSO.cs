using UnityEngine;
namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(WordSO), menuName = "Configs/" + nameof(WordSO))]
    public class WordSO : ScriptableObject
    {
        [field: SerializeField] public string JpReading { get; set; }
        [field: SerializeField] public LevelEnum Level { get; set; }
        [field: SerializeField] public string KanaReading { get; set; }
        [field: SerializeField] public string TranslationEng { get; set; }
        [field: SerializeField] public string TranslationRus { get; set; }
    }
}