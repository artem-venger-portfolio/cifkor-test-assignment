using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public sealed class UnityUINavigationPanelView : MonoBehaviour, INavigationPanelView
    {
        [SerializeField]
        private ToggleGroup _toggleGroup;

        [SerializeField]
        private UnityUINavigationPanelToggle _toggleTemplate;

        [SerializeField]
        private Transform _tabContainer;

        public void AddTab(string tabName, UnityUINavigationPanelTabViewBase tab)
        {
            tab.SetParent(_tabContainer);
            var toggle = Instantiate(_toggleTemplate, _toggleGroup.transform, worldPositionStays: false);
            toggle.SetPageName(tabName);
            toggle.SetToggleGroup(_toggleGroup);
            toggle.SetTab(tab);
            toggle.SubscribeToValueChange();
        }
    }
}