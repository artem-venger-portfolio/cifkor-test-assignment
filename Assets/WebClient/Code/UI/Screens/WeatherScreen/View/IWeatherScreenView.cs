using System.Collections.Generic;

namespace WebClient
{
    public interface IWeatherScreenView
    {
        public void DisplayPeriods(IReadOnlyList<WeatherPeriod> periods);
    }
}