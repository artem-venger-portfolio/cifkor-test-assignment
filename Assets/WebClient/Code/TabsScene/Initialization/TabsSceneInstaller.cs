using Zenject;

namespace WebClient
{
    public class TabsSceneInstaller : InstallerBase
    {
        private readonly TabsSceneReferences _sceneReferences;

        public TabsSceneInstaller(TabsSceneReferences sceneReferences)
        {
            _sceneReferences = sceneReferences;
        }

        public override void InstallBindings()
        {
            Container.Bind<MonoBehaviourFunctions>()
                     .FromNewComponentOnNewGameObject()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IProjectLogger>()
                     .To<UnityConsoleLogger>()
                     .AsSingle()
                     .NonLazy();

            InstallNavigationPanel();
            InstallWeatherTab();
        }

        private void InstallNavigationPanel()
        {
            Container.Bind<NavigationPanelModel>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<INavigationPanelView>()
                     .To<UnityUINavigationPanelView>()
                     .FromInstance(_sceneReferences.NavigationPanelView)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<NavigationPanelPresenter>()
                     .AsSingle()
                     .NonLazy();
        }

        private void InstallWeatherTab()
        {
            Container.Bind<WeatherScreenModel>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind(typeof(IWeatherScreenView), typeof(UnityUIWeatherScreenView))
                     .FromComponentInNewPrefab(_sceneReferences.WeatherTabViewTemplate)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<WeatherScreenPresenter>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}