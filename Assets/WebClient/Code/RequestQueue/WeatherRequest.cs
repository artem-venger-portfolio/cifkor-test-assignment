using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    public class WeatherRequest : IWebRequest
    {
        private const string URL = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly ITextureLoader _textureLoader;
        private readonly ITextureCache _textureCache;
        private UnityWebRequest _weatherRequest;
        private Coroutine _requestCoroutine;
        private bool _isDone;
        private bool _isInProgress;

        public WeatherRequest(MonoBehaviourFunctions monoBehaviourFunctions, ITextureLoader textureLoader,
                              ITextureCache textureCache)
        {
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _textureLoader = textureLoader;
            _textureCache = textureCache;
        }

        public RequestType Type => RequestType.Weather;

        public void Send()
        {
            _requestCoroutine = _monoBehaviourFunctions.RunCoroutine(GetRequestEnumerator());
        }

        public bool IsInProgress()
        {
            return _isInProgress;
        }

        public bool IsDone()
        {
            return _isDone;
        }

        public void Finish()
        {
            _weatherRequest.Dispose();
            _weatherRequest = null;
        }

        private IEnumerator GetRequestEnumerator()
        {
            _isDone = false;
            _isInProgress = true;

            _weatherRequest = UnityWebRequest.Get(URL);

            yield return _weatherRequest.SendWebRequest();

            if (_weatherRequest.result != UnityWebRequest.Result.Success)
            {
                _requestCoroutine = null;
                _isDone = true;
                _isInProgress = false;
                yield break;
            }

            var json = _weatherRequest.downloadHandler.text;
            var response = JsonUtility.FromJson<WeatherResponse>(json);
            var responsePeriods = response.properties.periods;

            foreach (var currentPeriod in responsePeriods)
            {
                var iconURL = currentPeriod.icon;
                if (IsTextureLoadingOrLoaded(iconURL))
                {
                    continue;
                }

                _textureLoader.StartNew(iconURL);
            }

            while (_textureLoader.IsAllLoaded() == false)
            {
                yield return null;
            }

            _textureLoader.Finish();
            _requestCoroutine = null;
            _isDone = true;
            _isInProgress = false;
        }

        private bool IsTextureLoadingOrLoaded(string url)
        {
            return _textureLoader.IsLoading(url) || _textureCache.Contains(url);
        }
    }
}