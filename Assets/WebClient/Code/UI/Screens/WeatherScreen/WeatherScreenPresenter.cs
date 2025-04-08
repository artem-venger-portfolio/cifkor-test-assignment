using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    public class WeatherScreenPresenter
    {
        private readonly WeatherScreenModel _model;
        private readonly WeatherScreenViewBase _view;
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private readonly List<UnityWebRequest> _textureLoadingRequest;
        private readonly Dictionary<string, Texture2D> _urlToTexture;
        private Coroutine _requestCoroutine;

        public WeatherScreenPresenter(WeatherScreenModel model, WeatherScreenViewBase view,
                                      MonoBehaviourFunctions monoBehaviourFunctions)
        {
            _model = model;
            _view = view;
            _monoBehaviourFunctions = monoBehaviourFunctions;
            _textureLoadingRequest = new List<UnityWebRequest>();
            _urlToTexture = new Dictionary<string, Texture2D>();

            _model.IsOpenChanged += InOpenChangedEventHandler;
        }

        private void InOpenChangedEventHandler(bool isOpen)
        {
            if (isOpen)
            {
                _view.Open();
                _requestCoroutine = _monoBehaviourFunctions.RunCoroutine(GetRequestCoroutine());
            }
            else
            {
                _view.Close();
                _monoBehaviourFunctions.KillCoroutine(_requestCoroutine);
            }
        }

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
            foreach (var currentPeriod in response.properties.periods)
            {
                var textureLoadingRequest = UnityWebRequestTexture.GetTexture(currentPeriod.icon);
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
            }
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
    }
}