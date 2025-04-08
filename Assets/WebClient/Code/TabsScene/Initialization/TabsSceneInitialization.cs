using System.Linq;
using Zenject;

namespace WebClient
{
    public class TabsSceneInitialization
    {
        private readonly TabsSceneReferences _sceneReferences;

        private ScreenTypesAndInstance[] _screenTypesAndInstances;
        private ViewBase[] _screenViews;
        private SceneContext _sceneContext;

        public TabsSceneInitialization(TabsSceneReferences sceneReferences)
        {
            _sceneReferences = sceneReferences;
        }

        public void Perform()
        {
            FindAndRecordScreenInstances();

            _sceneContext = SceneContext.Create();
            var sceneInstaller = new TabsSceneInstaller();
            _sceneContext.AddNormalInstaller(sceneInstaller);
            _sceneContext.Run();
        }

        private void FindAndRecordScreenInstances()
        {
            _screenViews = FindScreenViews();

            var screenTypes = new[]
            {
                MVPTypesGroup.Create<NavigationPanelModel, NavigationPanelViewBase, NavigationPanelPresenter>(),
            };

            _screenTypesAndInstances = new ScreenTypesAndInstance[screenTypes.Length];
            for (var i = 0; i < screenTypes.Length; i++)
            {
                _screenTypesAndInstances[i] = LinkScreenTypesWithViewInstance(screenTypes[i]);
            }
        }

        private ViewBase[] FindScreenViews()
        {
            return _sceneReferences.ScreensContainer.GetComponentsInChildren<ViewBase>(includeInactive: true);
        }

        private ScreenTypesAndInstance LinkScreenTypesWithViewInstance(MVPTypesGroup group)
        {
            var viewType = group.View;
            var viewInstance = _screenViews.First(v => viewType.IsAssignableFrom(v.GetType()));
            return new ScreenTypesAndInstance(group.Model, viewType, group.Presenter, viewInstance);
        }
    }
}