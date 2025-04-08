using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace WebClient
{
    public class WeatherScreenPresenter
    {
        private readonly WeatherScreenModel _model;
        private readonly WeatherScreenViewBase _view;
        private readonly MonoBehaviourFunctions _monoBehaviourFunctions;
        private Coroutine _requestCoroutine;

        public WeatherScreenPresenter(WeatherScreenModel model, WeatherScreenViewBase view,
                                      MonoBehaviourFunctions monoBehaviourFunctions)
        {
            _model = model;
            _view = view;
            _monoBehaviourFunctions = monoBehaviourFunctions;

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
                            break;
                    }
                }

                yield return new WaitForSeconds(data_update_time);
            }
        }
    }
}