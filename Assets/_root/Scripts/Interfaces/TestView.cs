using TMPro;
using UnityEngine;

namespace TestSpace
{
    public abstract class TestView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _questionNum;

        public void Init()
        {
            _questionNum.text = "0";
        }
        public abstract void NextQuestion(TestKanjiStruct kanjiToReadingStruct, int index);
        public abstract void ShowReading(Color color);
        public abstract void HideReading(Color color);
    }
}
