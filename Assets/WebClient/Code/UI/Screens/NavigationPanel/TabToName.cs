using System;
using UnityEngine;

namespace WebClient
{
    [Serializable]
    public class TabToName
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private GameObject _tab;

        public string Name => _name;

        public GameObject Tab => _tab;
    }
}