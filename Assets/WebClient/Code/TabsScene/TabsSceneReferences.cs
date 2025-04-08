using System;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabsSceneReferences
    {
        [SerializeField]
        private NavigationPanelViewBase _navigationPanelView;

        [SerializeField]
        private UnityUIWeatherScreenView _weatherTabViewTemplate;

        public NavigationPanelViewBase NavigationPanelView => _navigationPanelView;

        public UnityUIWeatherScreenView WeatherTabViewTemplate => _weatherTabViewTemplate;
    }
}