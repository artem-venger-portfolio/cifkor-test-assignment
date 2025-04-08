using UnityEngine;

namespace WebClient
{
    public readonly struct WeatherPeriod
    {
        public WeatherPeriod(Texture texture, float temperature, string unit)
        {
            Texture = texture;
            Temperature = temperature;
            Unit = unit;
        }

        private Texture Texture { get; }
        private float Temperature { get; }
        private string Unit { get; }
    }
}