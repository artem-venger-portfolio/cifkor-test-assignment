using Zenject;

namespace WebClient
{
    public class TabsSceneInitialization
    {
        private readonly TabsSceneReferences _sceneReferences;
        private SceneContext _sceneContext;

        public TabsSceneInitialization(TabsSceneReferences sceneReferences)
        {
            _sceneReferences = sceneReferences;
        }

        public void Perform()
        {
            _sceneContext = SceneContext.Create();

            var navigationPanelModel = new NavigationPanelModel();
            var navigationPanelView = _sceneReferences.NavigationPanelView;
            var navigationPanelPresenter = new NavigationPanelPresenter(navigationPanelModel, navigationPanelView,
                                                                        dogBreedsScreen: null, weatherScreen: null);

            var installers = new InstallerBase[]
            {
                new ScreensInstaller(_sceneReferences),
                new TabsSceneInstaller(),
            };
            foreach (var currentInstaller in installers)
            {
                _sceneContext.AddNormalInstaller(currentInstaller);
            }

            _sceneContext.Run();
        }
    }
}