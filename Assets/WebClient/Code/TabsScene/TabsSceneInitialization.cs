using Zenject;

namespace WebClient
{
    public class TabsSceneInitialization
    {
        private readonly TabsSceneReferences _sceneReferences;

        private MVPTypesGroup[] _screenTypes;
        private SceneContext _sceneContext;

        public TabsSceneInitialization(TabsSceneReferences sceneReferences)
        {
            _sceneReferences = sceneReferences;
        }

        public void Perform()
        {
            CreateCollectionOfScreenTypes();

            _sceneContext = SceneContext.Create();
            var sceneInstaller = new TabsSceneInstaller();
            _sceneContext.AddNormalInstaller(sceneInstaller);
            _sceneContext.Run();
        }

        private void CreateCollectionOfScreenTypes()
        {
            _screenTypes = new[]
            {
                MVPTypesGroup.Create<NavigationPanelModel, NavigationPanelViewBase, NavigationPanelPresenter>(),
            };
        }
    }
}