using System.Collections;
using UnityEngine;

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

            while (true)
            {
                Debug.Log(message: "Update data");
                yield return new WaitForSeconds(data_update_time);
            }
        }
    }
}