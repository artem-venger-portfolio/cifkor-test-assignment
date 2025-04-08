using System;

namespace WebClient
{
    [Serializable]
    public class WeatherResponsePeriod
    {
        public string name;
        public float temperature;
        public string temperatureUnit;
        public string icon;
    }
}