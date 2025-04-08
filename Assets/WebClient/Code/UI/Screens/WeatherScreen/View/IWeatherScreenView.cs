using System;
using System.Collections.Generic;

namespace WebClient
{
    public interface IWeatherScreenView
    {
        public event Action Shown;
        public event Action Hidden;
        public void DisplayPeriods(IReadOnlyList<WeatherPeriod> periods);
    }
}