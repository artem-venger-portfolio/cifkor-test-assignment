using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    [UsedImplicitly]
    public class WeatherScreenModel : TabModelBase
    {
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly List<UnityWebRequest> _textureLoadingRequest;
        private readonly Dictionary<string, Texture2D> _urlToTexture;
        private readonly List<WeatherPeriod> _periods;
        private Coroutine _requestCoroutine;

        public WeatherScreenModel(MonoBehaviourFunctions monoBehaviourFunctions)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _textureLoadingRequest = new List<UnityWebRequest>();
            _urlToTexture = new Dictionary<string, Texture2D>();
            _periods = new List<WeatherPeriod>();
        }

        protected override void OnOpen()
        {
            _requestCoroutine = _monoBehaviourFunctions.RunCoroutine(GetRequestCoroutine());
        }

        protected override void OnClose()
        {
            _monoBehaviourFunctions.KillCoroutine(_requestCoroutine);
        }

        public IReadOnlyList<WeatherPeriod> Periods => _periods;

        public event Action PeriodsUpdated;

        private IEnumerator GetRequestCoroutine()
        {
            const float data_update_time = 5f;
            const string uri = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

            while (true)
            {
                using (var webRequest = UnityWebRequest.Get(uri))
                {
                    yield return webRequest.SendWebRequest();

                    switch (webRequest.result)
                    {
                        case UnityWebRequest.Result.ConnectionError:
                            Debug.LogError(message: "ConnectionError");
                            break;
                        case UnityWebRequest.Result.DataProcessingError:
                            Debug.LogError(message: "DataProcessingError");
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            Debug.LogError(message: "ProtocolError");
                            break;
                        case UnityWebRequest.Result.Success:
                            Debug.Log(message: "Success");
                            yield return DisplayWeather(webRequest.downloadHandler.text);
                            break;
                    }
                }

                yield return new WaitForSeconds(data_update_time);
            }
        }

        private IEnumerator DisplayWeather(string json)
        {
            var response = JsonUtility.FromJson<WeatherResponse>(json);
            var responsePeriods = response.properties.periods;
            foreach (var currentPeriod in responsePeriods)
            {
                var iconURL = currentPeriod.icon;
                if (IsTextureLoadingOrLoaded(iconURL))
                {
                    continue;
                }

                var textureLoadingRequest = UnityWebRequestTexture.GetTexture(iconURL);
                textureLoadingRequest.SendWebRequest();
                _textureLoadingRequest.Add(textureLoadingRequest);
            }

            while (IsAllTexturesLoaded() == false)
            {
                yield return null;
            }

            foreach (var loadingRequest in _textureLoadingRequest)
            {
                if (loadingRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(loadingRequest.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(loadingRequest);
                    var url = loadingRequest.url;
                    _urlToTexture.Add(url, texture);
                    Debug.Log($"{url} loaded");
                }
                loadingRequest.Dispose();
            }
            _textureLoadingRequest.Clear();

            _periods.Clear();
            foreach (var currentResponsePeriod in responsePeriods)
            {
                var texture = _urlToTexture[currentResponsePeriod.icon];
                var temperature = currentResponsePeriod.temperature;
                var temperatureUnit = currentResponsePeriod.temperatureUnit;
                var period = new WeatherPeriod(texture, temperature, temperatureUnit);
                _periods.Add(period);
            }

            PeriodsUpdated?.Invoke();
        }

        private bool IsAllTexturesLoaded()
        {
            foreach (var currentRequest in _textureLoadingRequest)
            {
                if (currentRequest.isDone)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        private bool IsTextureLoadingOrLoaded(string url)
        {
            foreach (var currentRequest in _textureLoadingRequest)
            {
                if (currentRequest.url == url)
                {
                    return true;
                }
            }

            return _urlToTexture.ContainsKey(url);
        }
    }
}