using UnityEngine;

namespace TestSpace
{
    internal class ObjectTypePanelView : PanelView
    {
        [SerializeField] private Transform _btnHolderTransform;

        public void Init()
        {
            Hide();
        }

        public void ChangeVisibility()
        {
            if (this.gameObject.activeSelf)
                Hide();
            else
                Show();
        }

        private void OnDestroy()
        {
        }
    }
}
