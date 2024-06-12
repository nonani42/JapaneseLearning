using TMPro;
using UnityEngine;

namespace TestSpace
{
    public abstract class TestView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI header;
        [SerializeField] protected TextMeshProUGUI _questionNum;

        public TextMeshProUGUI Header { get => header; set => header = value; }

        public virtual void NextQuestion(TestKanjiStruct kanjiStruct, int index) { }
        public virtual void NextQuestion(TestKanaStruct kanaStruct, int index) { }
        public virtual void NextQuestion(TestKeyStruct keyStruct, int index) { }
        public abstract void ShowAnswer(Color color);
        public abstract void HideAnswer(Color color);
    }
}
