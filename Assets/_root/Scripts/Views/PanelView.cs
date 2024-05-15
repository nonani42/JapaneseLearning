using UnityEngine;

namespace TestSpace
{
    public class PanelView : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
