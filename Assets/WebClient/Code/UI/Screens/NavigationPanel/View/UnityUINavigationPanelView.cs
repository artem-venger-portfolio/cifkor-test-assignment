using System;
using UnityEngine;
using UnityEngine.UI;

namespace WebClient
{
    public sealed class UnityUINavigationPanelView : NavigationPanelViewBase
    {
        [SerializeField]
        private Toggle _weatherToggle;

        [SerializeField]
        private Toggle _dogBreedsToggle;

        private void Start()
        {
            _weatherToggle.onValueChanged.AddListener(WeatherToggleValueChanged);
            _dogBreedsToggle.onValueChanged.AddListener(DogBreedsToggleValueChanged);
        }

        public override event Action WeatherTabSelected;

        public override event Action DogBreedsTabSelected;

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