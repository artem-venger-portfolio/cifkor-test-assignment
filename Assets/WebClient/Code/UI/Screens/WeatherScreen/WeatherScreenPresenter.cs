using JetBrains.Annotations;
using UnityEngine;

namespace WebClient
{
    [UsedImplicitly]
    public class WeatherScreenPresenter
    {
        private readonly WeatherScreenModel _model;
        private readonly IWeatherScreenView _view;

        public WeatherScreenPresenter(WeatherScreenModel model, IWeatherScreenView view)
        {
            _model = model;
            _view = view;

            _view.Shown += ViewShownEventHandler;
            _view.Hidden += ViewHiddenEventHandler;
            _model.PeriodsUpdated += PeriodsUpdatedEventHandler;
        }

        private void PeriodsUpdatedEventHandler()
        {
            _view.DisplayPeriods(_model.Periods);
        }

        private void ViewShownEventHandler()
        {
            _model.StartUpdatingPeriods();
        }

        private void ViewHiddenEventHandler()
        {
            _model.StopUpdatingPeriods();
        }
    }
}