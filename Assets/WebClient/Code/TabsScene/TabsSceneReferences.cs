using System;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabsSceneReferences
    {
        [SerializeField]
        private UnityUINavigationPanelView _navigationPanelView;

        [SerializeField]
        private UnityUIWeatherScreenView _weatherTabViewTemplate;

        [SerializeField]
        private UnityUIDogBreedsScreenView _dogBreedsTabViewTemplate;

        public UnityUINavigationPanelView NavigationPanelView => _navigationPanelView;

        public UnityUIWeatherScreenView WeatherTabViewTemplate => _weatherTabViewTemplate;

        public UnityUIDogBreedsScreenView DogBreedsTabViewTemplate => _dogBreedsTabViewTemplate;
    }
}