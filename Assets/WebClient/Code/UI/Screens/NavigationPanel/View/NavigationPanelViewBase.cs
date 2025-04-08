using System;
using UnityEngine;

namespace WebClient
{
    public abstract class NavigationPanelViewBase : MonoBehaviour
    {
        public abstract event Action WeatherTabSelected;
        public abstract event Action DogBreedsTabSelected;
    }
}