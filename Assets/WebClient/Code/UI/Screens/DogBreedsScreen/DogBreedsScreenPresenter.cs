namespace WebClient
{
    public class DogBreedsScreenPresenter
    {
        private readonly DogBreedsScreenModel _model;
        private readonly IDogBreedsScreenView _view;

        public DogBreedsScreenPresenter(DogBreedsScreenModel model, IDogBreedsScreenView view)
        {
            _model = model;
            _view = view;
        }
    }
}