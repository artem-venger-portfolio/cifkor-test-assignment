using System;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabsSceneReferences
    {
        [SerializeField]
        private GameObject _screensContainer;

        [SerializeField]
        private Component[] _screenViews;

        public GameObject ScreensContainer => _screensContainer;

        public Component[] ScreenViews => _screenViews;
    }
}