using System.Linq;
using UnityEngine;
using Zenject;

namespace WebClient
{
    public class ScreensInstaller : InstallerBase
    {
        private readonly GameObject _screensContainer;
        private ScreenTypesAndInstance[] _screenTypesAndInstances;
        private ViewBase[] _screenViews;

        public ScreensInstaller(TabsSceneReferences sceneReferences)
        {
            _screensContainer = sceneReferences.ScreensContainer;
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
            _screenViews = FindScreenViews<ViewBase>();

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

        private T[] FindScreenViews<T>()
        {
            return _screensContainer.GetComponentsInChildren<T>(includeInactive: true);
        }

        private ScreenTypesAndInstance LinkScreenTypesWithViewInstance(MVPTypesGroup group)
        {
            var viewType = group.View;
            var viewInstance = _screenViews.First(v => viewType.IsAssignableFrom(v.GetType()));
            return new ScreenTypesAndInstance(group.Model, viewType, group.Presenter, viewInstance);
        }
    }
}