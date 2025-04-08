using UnityEngine;

namespace WebClient
{
    public sealed class UnityUIWeatherScreenView : WeatherScreenViewBase
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private WeatherEntry _weatherEntryTemplate;
    }
}