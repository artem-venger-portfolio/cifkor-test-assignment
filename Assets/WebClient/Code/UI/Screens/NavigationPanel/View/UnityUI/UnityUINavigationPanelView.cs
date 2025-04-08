using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private UnityUIWeatherScreenView _weatherTab;

        public void CreateTabs()
        {
            AddTab(tabName: "Weather", _weatherTab);
        }

        private void AddTab(string tabName, UnityUINavigationPanelTabViewBase tab)
        {
            tab.SetParent(_tabContainer);
            var toggle = Instantiate(_toggleTemplate, _toggleGroup.transform, worldPositionStays: false);
            toggle.SetPageName(tabName);
            toggle.SetToggleGroup(_toggleGroup);
            toggle.SetTab(tab);
            toggle.SubscribeToValueChange();
        }

        [Inject]
        private void InjectDependencies(UnityUIWeatherScreenView weatherTab)
        {
            _weatherTab = weatherTab;
        }
    }
}