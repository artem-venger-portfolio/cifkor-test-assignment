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

        public Texture Texture { get; }
        public float Temperature { get; }
        public string Unit { get; }
    }
}