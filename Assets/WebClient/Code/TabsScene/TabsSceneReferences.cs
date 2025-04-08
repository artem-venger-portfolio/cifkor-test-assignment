using System;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabsSceneReferences
    {
        [SerializeField]
        private GameObject _screensContainer;

        public GameObject ScreensContainer => _screensContainer;
    }
}