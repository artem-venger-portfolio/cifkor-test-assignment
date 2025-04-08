using System.Linq;
using Zenject;

namespace WebClient
{
    public class ScreensInstaller : InstallerBase
    {
        private readonly TabsSceneReferences _tabsSceneReferences;
        private ScreenTypesAndInstance[] _screenTypesAndInstances;

        public ScreensInstaller(TabsSceneReferences sceneReferences)
        {
            _tabsSceneReferences = sceneReferences;
        }

        public override void InstallBindings()
        {
            GetTypesAndInstancesForInstalling();

            foreach (var currentTypesAndInstance in _screenTypesAndInstances)
            {
                Container.Bind(currentTypesAndInstance.Model)
                         .AsSingle()
                         .NonLazy();

                Container.Bind(currentTypesAndInstance.Presenter)
                         .AsSingle()
                         .NonLazy();

                Container.Bind(currentTypesAndInstance.ViewType)
                         .FromInstance(currentTypesAndInstance.ViewInstance)
                         .AsSingle()
                         .NonLazy();
            }
        }

        private void GetTypesAndInstancesForInstalling()
        {
            var screenTypes = new[]
            {
                MVPTypesGroup.Create<NavigationPanelModel, NavigationPanelViewBase, NavigationPanelPresenter>(),
                MVPTypesGroup.Create<WeatherScreenModel, WeatherScreenViewBase, WeatherScreenPresenter>(),
                MVPTypesGroup.Create<DogBreedsScreenModel, DogBreedsScreenViewBase, DogBreedsScreenPresenter>(),
            };

            _screenTypesAndInstances = new ScreenTypesAndInstance[screenTypes.Length];
            for (var i = 0; i < screenTypes.Length; i++)
            {
                _screenTypesAndInstances[i] = LinkScreenTypesWithViewInstance(screenTypes[i]);
            }
        }

        private ScreenTypesAndInstance LinkScreenTypesWithViewInstance(MVPTypesGroup group)
        {
            var viewType = group.View;
            var viewInstance = _tabsSceneReferences.ScreenViews
                                                   .First(v => viewType.IsAssignableFrom(v.GetType()));
            return new ScreenTypesAndInstance(group.Model, viewType, group.Presenter, viewInstance);
        }
    }
}