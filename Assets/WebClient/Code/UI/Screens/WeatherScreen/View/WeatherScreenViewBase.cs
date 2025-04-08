using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    public abstract class WeatherScreenViewBase : UnityUINavigationPanelTabViewBase, IWeatherScreenView
    {
        public abstract void DisplayPeriods(IReadOnlyList<WeatherPeriod> periods);
    }
}