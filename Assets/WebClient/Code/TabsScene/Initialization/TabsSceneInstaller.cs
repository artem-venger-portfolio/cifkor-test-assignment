using System;
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

            Container.Bind<IRequestQueue>()
                     .To<RequestQueue>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IInfoPanel>()
                     .FromInstance(_sceneReferences.InfoPanel)
                     .AsSingle()
                     .NonLazy();

            InstallNavigationPanel();
            InstallWeatherTab();
            InstallDogBreedsTab();
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

            Container.BindFactory<Action<WeatherRequest>, WeatherRequest, WeatherRequest.Factory>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<TextureLoader>()
                     .AsTransient()
                     .NonLazy();

            Container.Bind<ITextureCache>()
                     .To<TextureCache>()
                     .AsSingle()
                     .NonLazy();
        }

        private void InstallDogBreedsTab()
        {
            Container.Bind<DogBreedsScreenModel>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind(typeof(IDogBreedsScreenView), typeof(UnityUIDogBreedsScreenView))
                     .FromComponentInNewPrefab(_sceneReferences.DogBreedsTabViewTemplate)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<DogBreedsScreenPresenter>()
                     .AsSingle()
                     .NonLazy();

            Container.BindFactory<Action<DogBreedsRequest>, DogBreedsRequest, DogBreedsRequest.Factory>()
                     .AsSingle()
                     .NonLazy();

            Container.BindFactory<string, Action<DogBreedDescriptionRequest>, DogBreedDescriptionRequest,
                          DogBreedDescriptionRequest.Factory>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}