using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class StatTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statText;

        public TextMeshProUGUI StatText { get => _statText; set => _statText = value; }

        public void TurnOn(bool isOn) => gameObject.SetActive(isOn);
    }
}
