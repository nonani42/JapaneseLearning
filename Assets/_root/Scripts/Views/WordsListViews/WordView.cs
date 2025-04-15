using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class WordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _wordText;
        [SerializeField] private TextMeshProUGUI _kanaReadingText;
        [SerializeField] private TextMeshProUGUI _transRusText;
        [SerializeField] private TextMeshProUGUI _transEngText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _background;

        [SerializeField] private Color _knownWordColor;
        [SerializeField] private Color _unknownWordColor;

        public void SetWord(WordSO wordSO, bool isKnown)
        {
            _kanaReadingText.text = wordSO.KanaReading;
            _transRusText.text = wordSO.TranslationRus;
            _transEngText.text = wordSO.TranslationEng;
            _levelText.text = wordSO.Level.ToString();

            string word = wordSO.SpecialReading ? wordSO.JpReading + "*" : wordSO.JpReading;
            _wordText.text = word;

            _background.color = isKnown ? _knownWordColor : _unknownWordColor;
        }

        public void TurnOn(bool isOn) => gameObject.SetActive(isOn);
    }
}
