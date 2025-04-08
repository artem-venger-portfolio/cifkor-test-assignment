using UnityEngine;

namespace WebClient
{
    public abstract class NavigationPanelViewBase : MonoBehaviour
    {
        public abstract void AddTab(string tabName, UnityUINavigationPanelTabViewBase tab);
    }
}