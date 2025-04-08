using System;

namespace WebClient
{
    [Serializable]
    public class WeatherResponsePeriod
    {
        public float temperature;
        public string temperatureUnit;
        public string icon;
    }
}