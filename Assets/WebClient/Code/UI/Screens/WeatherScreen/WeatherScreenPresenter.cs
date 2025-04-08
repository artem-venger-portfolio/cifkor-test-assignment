namespace WebClient
{
    public class WeatherScreenPresenter
    {
        private readonly WeatherScreenModel _model;
        private readonly WeatherScreenViewBase _view;

        public WeatherScreenPresenter(WeatherScreenModel model, WeatherScreenViewBase view)
        {
            _model = model;
            _view = view;

            _model.IsOpenChanged += InOpenChangedEventHandler;
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
    }
}