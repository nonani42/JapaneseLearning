using System;
using TestSpace;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(KanjiCardSO), menuName = "Configs/" + nameof(KanjiCardSO))]
public class KanjiCardSO : ScriptableObject
{
    [field: SerializeField] public char Kanji { get; private set; }
    [field: SerializeField] public LevelEnum Level { get; private set; }
    [field: SerializeField] public GradeEnum Grade { get; private set; }
    [field: SerializeField] public int Strokes { get; private set; }
    [field: SerializeField] public string UpperReading { get; private set; }
    [field: SerializeField] public string LowerReading { get; private set; }
    [field: SerializeField] public string MeaningEng { get; private set; }
    [field: SerializeField] public string MeaningRus { get; private set; }
    [field: SerializeField] public Sprite StrokeOrder { get; private set; }
    [field: SerializeField] public KeySO Key { get; private set; }
    [field: SerializeField] public WordSO[] Examples { get; private set; }
}

[CreateAssetMenu(fileName = nameof(WordSO), menuName = "Configs/" + nameof(WordSO))]
public class WordSO : ScriptableObject
{
    [field: SerializeField] public LevelEnum Level { get; private set; }
    [field: SerializeField] public string JpReading { get; private set; }
    [field: SerializeField] public string KanaReading { get; private set; }
    [field: SerializeField] public string TranslationEng { get; private set; }
    [field: SerializeField] public string TranslationRus { get; private set; }
}

[CreateAssetMenu(fileName = nameof(KeySO), menuName = "Configs/" + nameof(KeySO))]
public class KeySO : ScriptableObject
{
    [field: SerializeField] public string KeyName { get; private set; }
    [field: SerializeField] public string Reading { get; private set; }
    [field: SerializeField] public string TranslationEng { get; private set; }
    [field: SerializeField] public string TranslationRus { get; private set; }
}
