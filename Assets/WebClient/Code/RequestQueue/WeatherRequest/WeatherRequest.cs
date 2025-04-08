using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace WebClient
{
    public class WeatherRequest : IWebRequest
    {
        private const string URL = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly TextureLoader _textureLoader;
        private readonly ITextureCache _textureCache;
        private readonly IProjectLogger _logger;
        private WeatherResponsePeriod[] _responsePeriods;
        private UnityWebRequest _weatherRequest;
        private Coroutine _requestCoroutine;
        private bool _isInProgress;

        public WeatherRequest(MonoBehaviourFunctions monoBehaviourFunctions, TextureLoader textureLoader,
                              ITextureCache textureCache, IProjectLogger logger)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            Result = new List<WeatherPeriod>();
            _textureLoader = textureLoader;
            _textureCache = textureCache;
            _logger = logger;
        }

        public List<WeatherPeriod> Result { get; }

        public RequestType Type => RequestType.Weather;

        public void Send()
        {
            _requestCoroutine = _monoBehaviourFunctions.RunCoroutine(GetRequestEnumerator());
        }

        public bool IsInProgress()
        {
            return _isInProgress;
        }

        public void Interrupt()
        {
            if (IsInProgress() == false)
            {
                return;
            }

            if (_requestCoroutine != null)
            {
                _monoBehaviourFunctions.KillCoroutine(_requestCoroutine);
                _requestCoroutine = null;
            }

            if (_textureLoader.IsOperating())
            {
                _textureLoader.Stop();
            }

            _weatherRequest.Dispose();
            _weatherRequest = null;
        }

        private IEnumerator GetRequestEnumerator()
        {
            _isInProgress = true;

            _weatherRequest = UnityWebRequest.Get(URL);

            yield return _weatherRequest.SendWebRequest();

            if (_weatherRequest.result != UnityWebRequest.Result.Success)
            {
                _logger.LogError(message: "WeatherRequest failed!");
                _requestCoroutine = null;
                _isInProgress = false;
                yield break;
            }

            var json = _weatherRequest.downloadHandler.text;
            var response = JsonUtility.FromJson<WeatherResponse>(json);
            _responsePeriods = response.properties.periods;

            _textureLoader.Start();

            foreach (var currentPeriod in _responsePeriods)
            {
                var iconURL = currentPeriod.icon;
                if (IsTextureLoadingOrLoaded(iconURL))
                {
                    continue;
                }

                _textureLoader.Load(iconURL);
            }

            while (_textureLoader.IsLoading())
            {
                yield return null;
            }

            _textureLoader.Stop();

            FillResult();

            _requestCoroutine = null;
            _isInProgress = false;
        }

        private bool IsTextureLoadingOrLoaded(string url)
        {
            return _textureLoader.IsLoading(url) || _textureCache.Contains(url);
        }

        private void FillResult()
        {
            Result.Clear();
            foreach (var currentResponsePeriod in _responsePeriods)
            {
                var texture = _textureCache.Get(currentResponsePeriod.icon);
                var temperature = currentResponsePeriod.temperature;
                var temperatureUnit = currentResponsePeriod.temperatureUnit;
                var period = new WeatherPeriod(texture, temperature, temperatureUnit);
                Result.Add(period);
            }
        }

        public class Factory : PlaceholderFactory<WeatherRequest>
        {
        }
    }
}