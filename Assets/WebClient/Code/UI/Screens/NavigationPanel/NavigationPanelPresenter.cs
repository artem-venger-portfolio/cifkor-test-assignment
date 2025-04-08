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
        }
    }
}