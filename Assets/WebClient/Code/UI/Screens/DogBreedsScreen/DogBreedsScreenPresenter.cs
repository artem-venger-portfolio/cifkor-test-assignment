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

            _view.Shown += ViewShownEventHandler;
            _view.Hidden += ViewHiddenEventHandler;
            _view.BreedClicked += BreedClickedEventHandler;
            _model.BreedsReceived += BreedsReceivedEventHandler;
            _model.DescriptionReceived += DescriptionReceivedEventHandler;
        }

        private void ViewShownEventHandler()
        {
            _model.GetBreeds();
        }

        private void ViewHiddenEventHandler()
        {
            _model.InterruptRequestsIfNeeded();
        }

        private void BreedsReceivedEventHandler()
        {
            _view.DisplayBreeds(_model.Breeds);
        }

        private void BreedClickedEventHandler(int index)
        {
            _model.GetBreedDescription(index);
        }

        private void DescriptionReceivedEventHandler(DogBreedDescription description)
        {
        }
    }
}