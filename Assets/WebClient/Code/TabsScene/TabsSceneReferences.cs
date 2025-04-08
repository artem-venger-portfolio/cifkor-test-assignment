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
        private WeatherScreenViewBase _weatherTabViewTemplate;

        public NavigationPanelViewBase NavigationPanelView => _navigationPanelView;

        public WeatherScreenViewBase WeatherTabViewTemplate => _weatherTabViewTemplate;
    }
}