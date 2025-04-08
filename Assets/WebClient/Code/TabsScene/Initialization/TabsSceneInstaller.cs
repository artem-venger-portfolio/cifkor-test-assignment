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

            InstallNavigationPanel();
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
    }
}