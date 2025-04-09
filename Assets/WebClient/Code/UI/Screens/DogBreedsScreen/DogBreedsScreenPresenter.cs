namespace WebClient
{
    public class DogBreedsScreenPresenter
    {
        private readonly DogBreedsScreenModel _model;
        private readonly IDogBreedsScreenView _view;
        private readonly IInfoPanel _infoPanel;

        public DogBreedsScreenPresenter(DogBreedsScreenModel model, IDogBreedsScreenView view, IInfoPanel infoPanel)
        {
            _model = model;
            _view = view;
            _infoPanel = infoPanel;

            _view.Shown += ViewShownEventHandler;
            _view.Hidden += ViewHiddenEventHandler;
            _view.BreedClicked += BreedClickedEventHandler;
            _model.BreedsReceived += BreedsReceivedEventHandler;
            _model.DescriptionReceived += DescriptionReceivedEventHandler;
            _model.DescriptionLoadingStarted += DescriptionLoadingStartedEventHandler;
            _model.DescriptionLoadingFinished += DescriptionLoadingFinishedEventHandler;
        }

        private void ViewShownEventHandler()
        {
            _model.GetBreeds();
            _view.SetLoadingScreenActive(isActive: true);
        }

        private void ViewHiddenEventHandler()
        {
            _model.InterruptRequestsIfNeeded();
        }

        private void BreedsReceivedEventHandler()
        {
            _view.DisplayBreeds(_model.Breeds);
            _view.SetLoadingScreenActive(isActive: false);
        }

        private void BreedClickedEventHandler(int index)
        {
            _model.GetBreedDescription(index);
        }

        private void DescriptionReceivedEventHandler(DogBreedDescription description)
        {
            _infoPanel.Open(description.Name, description.Description);
        }

        private void DescriptionLoadingFinishedEventHandler(int index)
        {
            _view.ShowLoadingIndicator(index);
        }

        private void DescriptionLoadingStartedEventHandler(int index)
        {
            _view.HideLoadingIndicator(index);
        }
    }
}