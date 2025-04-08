namespace WebClient
{
    public class DogBreedsScreenPresenter
    {
        private readonly DogBreedsScreenModel _model;
        private readonly DogBreedsScreenViewBase _view;

        public DogBreedsScreenPresenter(DogBreedsScreenModel model, DogBreedsScreenViewBase view)
        {
            _model = model;
            _view = view;
        }
    }
}