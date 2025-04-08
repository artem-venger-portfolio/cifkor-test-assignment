using UnityEngine;

namespace WebClient
{
    public class NavigationPanelPresenter
    {
        private readonly NavigationPanelModel _model;
        private readonly NavigationPanelViewBase _view;

        public NavigationPanelPresenter(NavigationPanelModel model, NavigationPanelViewBase view)
        {
            _model = model;
            _view = view;

            _view.WeatherTabSelected += WeatherTabSelectedEventHandler;
            _view.DogBreedsTabSelected += DogBreedsTabSelectedEventHandler;
        }

        private void WeatherTabSelectedEventHandler()
        {
            LogInfo(nameof(WeatherTabSelectedEventHandler));
        }

        private void DogBreedsTabSelectedEventHandler()
        {
            LogInfo(nameof(DogBreedsTabSelectedEventHandler));
        }

        private void LogInfo(string message)
        {
            Debug.Log($"[{nameof(NavigationPanelPresenter)}] {message}");
        }
    }
}