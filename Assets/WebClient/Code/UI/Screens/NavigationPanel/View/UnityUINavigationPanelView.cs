using System;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public sealed class UnityUINavigationPanelView : NavigationPanelViewBase
    {
        [SerializeField]
        private TabToName[] _tabToNameCollection;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        [SerializeField]
        private Toggle _weatherToggle;

        [SerializeField]
        private Toggle _dogBreedsToggle;

        [SerializeField]
        private NavigationPanelToggle _toggleTemplate;

        private void Start()
        {
            _weatherToggle.onValueChanged.AddListener(WeatherToggleValueChanged);
            _dogBreedsToggle.onValueChanged.AddListener(DogBreedsToggleValueChanged);
        }

        public override event Action WeatherTabSelected;

        public override event Action DogBreedsTabSelected;

        public override void AddTab(GameObject tab)
        {
            tab.transform.SetParent(_toggleGroup.transform, false);
        }

        public void AddTab(string pageName, GameObject tabView)
        {
        }

        private void WeatherToggleValueChanged(bool isOn)
        {
            if (isOn == false)
            {
                return;
            }

            WeatherTabSelected?.Invoke();
        }

        private void DogBreedsToggleValueChanged(bool isOn)
        {
            if (isOn == false)
            {
                return;
            }

            DogBreedsTabSelected?.Invoke();
        }
    }
}