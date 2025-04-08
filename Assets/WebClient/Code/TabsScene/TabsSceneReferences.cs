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

        public Component[] ScreenViews => _screenViews;

        public IList<TabToName> TabToNameCollection => _tabToNameCollection;
    }
}