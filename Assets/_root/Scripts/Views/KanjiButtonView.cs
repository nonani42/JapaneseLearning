using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestSpace
{
    internal class KanjiButtonView : MonoBehaviour
    {
        [SerializeField] private Button _kanjiButton;
        [SerializeField] private TextMeshProUGUI _kanjiButtonText;
        [SerializeField] private Color _unknownKanjiColor;
        [SerializeField] private Color _knownKanjiColor;

        private bool _isKnown;

        public bool IsKnown
        {
            get => _isKnown; 
            private set
            {
                _isKnown = value;
                OnSelectionChanged?.Invoke(_kanjiButtonText.text, _isKnown);
            }
        }

        public event Action<string, bool> OnSelectionChanged;

        private void Start() => _kanjiButton.onClick.AddListener(ChangeColor);

        public void SetButton(string kanjiName, bool isKnown)
        {
            _kanjiButtonText.text = kanjiName;
            _isKnown = isKnown;
            _kanjiButton.image.color = _isKnown ? _knownKanjiColor : _unknownKanjiColor;
        }

        public void TurnOn(bool isOn)
        {
            gameObject.SetActive(isOn);
        }

        private void ChangeColor()
        {
            IsKnown = !IsKnown;
            _kanjiButton.image.color = IsKnown ? _knownKanjiColor : _unknownKanjiColor;
        }

        private void OnDestroy()
        {
            _kanjiButton.onClick.RemoveAllListeners();
        }
    }
}
