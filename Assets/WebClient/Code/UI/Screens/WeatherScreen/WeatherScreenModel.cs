using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace WebClient
{
    [UsedImplicitly]
    public class WeatherScreenModel
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly WeatherRequest.Factory _requestFactory;
        private readonly List<WeatherPeriod> _periods;
        private readonly IRequestQueue _requestQueue;
        private readonly IProjectLogger _logger;
        private Coroutine _requestCoroutine;

        public WeatherScreenModel(MonoBehaviourFunctions monoBehaviourFunctions, WeatherRequest.Factory requestFactory,
                                  IRequestQueue requestQueue, IProjectLogger logger)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _requestFactory = requestFactory;
            _requestQueue = requestQueue;
            _logger = logger;
            _periods = new List<WeatherPeriod>();
        }

        public IReadOnlyList<WeatherPeriod> Periods => _periods;

        public event Action PeriodsUpdated;

        public void StartUpdatingPeriods()
        {
            LogInfo(nameof(StartUpdatingPeriods));
            _requestCoroutine = _monoBehaviourFunctions.RunCoroutine(GetRequestCoroutine());
        }

        public void StopUpdatingPeriods()
        {
            LogInfo(nameof(StopUpdatingPeriods));
            _monoBehaviourFunctions.KillCoroutine(_requestCoroutine);
            _requestQueue.Interrupt(RequestType.Weather);
        }

        private IEnumerator GetRequestCoroutine()
        {
            const float data_update_time = 5f;

            while (true)
            {
                LogInfo(message: "Creating request");
                var request = _requestFactory.Create(RequestCompletedEventHandler);
                _requestQueue.Add(request);

                yield return new WaitForSeconds(data_update_time);
            }
        }

        private void RequestCompletedEventHandler(WeatherRequest request)
        {
            LogInfo(nameof(RequestCompletedEventHandler));
            _periods.Clear();
            _periods.AddRange(request.Result);
            PeriodsUpdated?.Invoke();
        }

        private void LogInfo(string message)
        {
            _logger.LogInfo($"[{nameof(WeatherScreenModel)}] {message}");
        }
    }
}