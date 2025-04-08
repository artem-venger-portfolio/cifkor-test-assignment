using UnityEngine;

namespace WebClient
{
    public abstract class UnityUINavigationPanelTabViewBase : MonoBehaviour, INavigationPanelTabView
    {
        public void Show()
        {
            SetActive(isActive: true);
        }

        public void Hide()
        {
            SetActive(isActive: false);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}