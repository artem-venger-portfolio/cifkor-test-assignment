namespace WebClient
{
    public class NavigationPanelPresenter
    {
        private readonly NavigationPanelModel _model;
        private readonly INavigationPanelView _view;

        public NavigationPanelPresenter(NavigationPanelModel model, INavigationPanelView view)
        {
            _model = model;
            _view = view;
        }
    }
}