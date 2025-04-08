using UnityEngine;

namespace WebClient
{
    public abstract class UnityUINavigationPanelTabViewBase : MonoBehaviour
    {
        public void Show()
        {
            OnShow();
            SetActive(isActive: true);
        }

        public void Hide()
        {
            OnHide();
            SetActive(isActive: false);
        }

        public void SetParent(Transform newParent)
        {
            transform.SetParent(newParent, worldPositionStays: false);
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}