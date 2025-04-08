using UnityEngine;

namespace WebClient
{
    public class NavigationPanelPresenter
    {
        private readonly NavigationPanelModel _model;
        private readonly NavigationPanelViewBase _view;
        private readonly DogBreedsScreenModel _dogBreedsScreen;
        private readonly WeatherScreenModel _weatherScreen;

        public NavigationPanelPresenter(NavigationPanelModel model, NavigationPanelViewBase view,
                                        DogBreedsScreenModel dogBreedsScreen, WeatherScreenModel weatherScreen)
        {
            _model = model;
            _view = view;
            _dogBreedsScreen = dogBreedsScreen;
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