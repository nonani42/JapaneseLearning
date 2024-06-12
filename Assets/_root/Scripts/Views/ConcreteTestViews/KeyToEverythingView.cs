using System.Text;
using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class KeyToEverythingView : TestView
    {
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _keyNameText;
        [SerializeField] private TextMeshProUGUI _keyVersionsText;
        [SerializeField] private TextMeshProUGUI _readingText;
        [SerializeField] private TextMeshProUGUI _engTranslationText;
        [SerializeField] private TextMeshProUGUI _rusTranslationText;
        [SerializeField] private TextMeshProUGUI _examplesText;
        [SerializeField] private TextMeshProUGUI _importanceText;

        public override void NextQuestion(TestKeyStruct keyStruct, int index)
        {
            _questionText.text = new StringBuilder().Append("#").Append(keyStruct.Key.Num.ToString()).ToString();
            _keyNameText.text = keyStruct.Key.KeyName;
            _keyVersionsText.text = keyStruct.Key.KeyVersions;
            _readingText.text = keyStruct.Key.Reading;
            _engTranslationText.text = keyStruct.Key.TranslationEng;
            _rusTranslationText.text = keyStruct.Key.TranslationRus;
            _examplesText.text = keyStruct.Key.ExampleKanji;
            _importanceText.text = keyStruct.Key.IsImportant ? "important" : "";

            _questionNum.text = index.ToString();
        }

        public override void ShowAnswer(Color color)
        {
            _keyNameText.color = color;
            _keyVersionsText.color = color;
            _readingText.color = color;
            _engTranslationText.color = color;
            _rusTranslationText.color = color;
            _examplesText.color = color;
            _importanceText.color = color;
        }

        public override void HideAnswer(Color color)
        {
            _keyNameText.color = color;
            _keyVersionsText.color = color;
            _readingText.color = color;
            _engTranslationText.color = color;
            _rusTranslationText.color = color;
            _examplesText.color = color;
            _importanceText.color = color;
        }
    }
}
