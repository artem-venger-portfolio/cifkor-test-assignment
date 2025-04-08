using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public sealed class UnityUINavigationPanelView : NavigationPanelViewBase
    {
        [SerializeField]
        private ToggleGroup _toggleGroup;

        [SerializeField]
        private NavigationPanelToggle _toggleTemplate;

        [SerializeField]
        private Transform _tabContainer;

        public override void AddTab(string tabName, UnityUINavigationPanelTabViewBase tab)
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