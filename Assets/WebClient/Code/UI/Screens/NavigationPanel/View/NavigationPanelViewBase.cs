using System;

namespace WebClient
{
    public abstract class NavigationPanelViewBase : ViewBase
    {
        public abstract event Action WeatherTabSelected;
        public abstract event Action DogBreedsTabSelected;
    }
}