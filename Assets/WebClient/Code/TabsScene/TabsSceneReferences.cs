using System;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabsSceneReferences
    {
        [SerializeField]
        private Component[] _screenViews;

        public Component[] ScreenViews => _screenViews;
    }
}