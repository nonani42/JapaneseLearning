using TMPro;
using UnityEngine;

namespace TestSpace
{
    public abstract class TestView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI header;
        [SerializeField] protected string testName;
        [SerializeField] protected TextMeshProUGUI _questionNum;

        protected TextMeshProUGUI Header { get => header; private set => header = value; }
        public string TestName { get => testName; private set => testName = value; }

        public abstract void Init();
        public abstract void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index);
        public abstract void ShowAnswer(Color color);
        public abstract void HideAnswer(Color color);
    }
}
