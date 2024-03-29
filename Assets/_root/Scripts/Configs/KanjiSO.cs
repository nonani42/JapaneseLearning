using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(KanjiSO), menuName = "Configs/" + nameof(KanjiSO))]
public class KanjiSO : ScriptableObject
{
    [field: SerializeField] public string Kanji { get; private set; }
    [field: SerializeField] public int Strokes { get; private set; }
    [field: SerializeField] public string UpperReading { get; private set; }
    [field: SerializeField] public string LowerReading { get; private set; }
    [field: SerializeField] public string MeaningEng { get; private set; }
    [field: SerializeField] public string MeaningRus { get; private set; }
    [field: SerializeField] public Sprite StrokeOrder { get; private set; }
    [field: SerializeField] public Sprite Key { get; private set; }
    [field: SerializeField] public ExampleSO[] Examples { get; private set; }
}

[Serializable]
public class ExampleSO
{
    [field: SerializeField] public string Example { get; private set; }
    [field: SerializeField] public string TranslationEng { get; private set; }
    [field: SerializeField] public string TranslationRus { get; private set; }
}
