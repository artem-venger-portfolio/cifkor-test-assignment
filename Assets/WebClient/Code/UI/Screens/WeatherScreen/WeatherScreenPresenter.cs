using JetBrains.Annotations;
using UnityEngine;

namespace WebClient
{
    [UsedImplicitly]
    public class WeatherScreenPresenter
    {
        private readonly WeatherScreenModel _model;
        private readonly WeatherScreenViewBase _view;
        private Coroutine _requestCoroutine;

        public WeatherScreenPresenter(WeatherScreenModel model, WeatherScreenViewBase view)
        {
            _model = model;
            _view = view;

            _model.IsOpenChanged += InOpenChangedEventHandler;
            _model.PeriodsUpdated += PeriodsUpdatedEventHandler;
        }

        private void InOpenChangedEventHandler(bool isOpen)
        {
            if (isOpen)
            {
                _view.Open();
            }
            else
            {
                _view.Close();
            }
        }

        private void PeriodsUpdatedEventHandler()
        {
            _view.DisplayPeriods(_model.Periods);
        }
    }
}