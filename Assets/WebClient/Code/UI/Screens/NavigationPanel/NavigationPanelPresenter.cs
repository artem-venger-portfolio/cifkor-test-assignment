using UnityEngine;

namespace WebClient
{
    public class NavigationPanelPresenter
    {
        private readonly NavigationPanelModel _model;
        private readonly NavigationPanelViewBase _view;
        private readonly WeatherScreenModel _weatherScreen;

        public NavigationPanelPresenter(NavigationPanelModel model, NavigationPanelViewBase view,
                                        WeatherScreenModel weatherScreen)
        {
            _model = model;
            _view = view;
            _weatherScreen = weatherScreen;

            _view.WeatherTabSelected += WeatherTabSelectedEventHandler;
            _view.DogBreedsTabSelected += DogBreedsTabSelectedEventHandler;
        }

        private void WeatherTabSelectedEventHandler()
        {
            LogInfo(nameof(WeatherTabSelectedEventHandler));
            _weatherScreen.Open();
        }

        private void DogBreedsTabSelectedEventHandler()
        {
            LogInfo(nameof(DogBreedsTabSelectedEventHandler));
            _weatherScreen.Close();
        }

        private void LogInfo(string message)
        {
            Debug.Log($"[{nameof(NavigationPanelPresenter)}] {message}");
        }
    }
}