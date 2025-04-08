using UnityEngine;

namespace WebClient
{
    public abstract class NavigationPanelViewBase : MonoBehaviour, INavigationPanelView
    {
        public abstract void AddTab(string tabName, UnityUINavigationPanelTabViewBase tab);
    }
}