using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    public sealed class UnityUIWeatherScreenView : WeatherScreenViewBase
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private WeatherEntry _weatherEntryTemplate;

        private readonly List<WeatherEntry> _entries = new();

        public override void Open()
        {
            SetActive(isActive: true);
        }

        public override void Close()
        {
            SetActive(isActive: false);
        }

        public override void DisplayPeriods(List<WeatherPeriod> periods)
        {
            foreach (var currentEntry in _entries)
            {
                currentEntry.gameObject.SetActive(value: false);
            }

            for (var i = 0; i < periods.Count; i++)
            {
                var currentPeriodData = periods[i];
                if (i >= _entries.Count)
                {
                    _entries.Add(Instantiate(_weatherEntryTemplate, _content));
                }

                var currentWeatherEntry = _entries[i];
                currentWeatherEntry.SetTexture(currentPeriodData.Texture);
                currentWeatherEntry.SetTemperature(currentPeriodData.Temperature, currentPeriodData.Unit);
                currentWeatherEntry.gameObject.SetActive(value: true);
            }
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}