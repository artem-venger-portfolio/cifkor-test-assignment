using UnityEngine;

namespace WebClient
{
    public readonly struct WeatherPeriod
    {
        public WeatherPeriod(Texture2D texture, float temperature, string unit)
        {
            Texture = texture;
            Temperature = temperature;
            Unit = unit;
        }

        public Texture2D Texture { get; }
        public float Temperature { get; }
        public string Unit { get; }
    }
}