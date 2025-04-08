using System;
using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabsSceneReferences
    {
        [SerializeField]
        private Component[] _screenViews;

        [SerializeField]
        private TabToName[] _tabToNameCollection;

        [SerializeField]
        private NavigationPanelViewBase _navigationPanelView;

        [SerializeField]
        private WeatherScreenViewBase _weatherTabViewTemplate;

        public Component[] ScreenViews => _screenViews;

        public IList<TabToName> TabToNameCollection => _tabToNameCollection;

        public NavigationPanelViewBase NavigationPanelView => _navigationPanelView;

        public WeatherScreenViewBase WeatherTabViewTemplate => _weatherTabViewTemplate;
    }
}