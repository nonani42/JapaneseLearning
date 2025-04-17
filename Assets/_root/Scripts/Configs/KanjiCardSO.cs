using UnityEngine;
namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(KanjiCardSO), menuName = "Configs/" + nameof(KanjiCardSO))]
    public class KanjiCardSO : ScriptableObject, IStat
    {
        [field: SerializeField] public char Kanji { get; set; }
        [field: SerializeField] public LevelEnum Level { get; set; }
        [field: SerializeField] public GradeEnum Grade { get; set; }
        [field: SerializeField] public int Strokes { get; set; }
        [field: SerializeField] public string UpperReading { get; set; }
        [field: SerializeField] public string LowerReading { get; set; }
        [field: SerializeField] public string MeaningEng { get; set; }
        [field: SerializeField] public string MeaningRus { get; set; }
        [field: SerializeField] public Sprite StrokeOrder { get; set; }
        [field: SerializeField] public KeySO Key { get; set; }
        [field: SerializeField] public WordSO[] Examples { get; set; }
        [field: SerializeField] public bool IsRepeat { get; set; }
    }
}
