using UnityEngine;

namespace WebClient
{
    public readonly struct WeatherPeriod
    {
        public WeatherPeriod(string name, Texture2D texture, float temperature, string unit)
        {
            Name = name;
            Texture = texture;
            Temperature = temperature;
            Unit = unit;
        }

        public string Name { get; }
        public Texture2D Texture { get; }
        public float Temperature { get; }
        public string Unit { get; }
    }
}