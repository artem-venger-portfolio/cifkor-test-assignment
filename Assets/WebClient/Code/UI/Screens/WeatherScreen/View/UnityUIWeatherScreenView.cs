﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace WebClient
{
    public sealed class UnityUIWeatherScreenView : UnityUINavigationPanelTabViewBase, IWeatherScreenView
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private WeatherEntry _weatherEntryTemplate;

        private readonly List<WeatherEntry> _entries = new();

        public event Action Shown;

        public event Action Hidden;

        public void DisplayPeriods(IReadOnlyList<WeatherPeriod> periods)
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
                currentWeatherEntry.SetTemperature(currentPeriodData.Name, currentPeriodData.Temperature,
                                                   currentPeriodData.Unit);
                currentWeatherEntry.gameObject.SetActive(value: true);
            }
        }

        protected override void OnShow()
        {
            Shown?.Invoke();
        }

        protected override void OnHide()
        {
            Hidden?.Invoke();
        }
    }
}