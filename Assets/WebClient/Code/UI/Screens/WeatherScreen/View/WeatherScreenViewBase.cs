using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    public abstract class WeatherScreenViewBase : UnityUINavigationPanelTabViewBase
    {
        public abstract void DisplayPeriods(IReadOnlyList<WeatherPeriod> periods);
    }
}