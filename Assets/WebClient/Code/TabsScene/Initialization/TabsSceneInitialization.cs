using UnityEngine;
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

            var monoBehaviourFunctionsGO = new GameObject(nameof(MonoBehaviourFunctions));
            var monoBehaviourFunctions = monoBehaviourFunctionsGO.AddComponent<MonoBehaviourFunctions>();

            var weatherTabModel = new WeatherScreenModel(monoBehaviourFunctions);
            var weatherTabView = Object.Instantiate(_sceneReferences.WeatherTabViewTemplate);
            var weatherTabPresenter = new WeatherScreenPresenter(weatherTabModel, weatherTabView);

            var navigationPanelModel = new NavigationPanelModel();
            var navigationPanelView = _sceneReferences.NavigationPanelView;
            var navigationPanelPresenter = new NavigationPanelPresenter(navigationPanelModel, navigationPanelView);

            navigationPanelView.AddTab(tabName: "Weather", weatherTabView);

            var installers = new InstallerBase[]
            {
                new TabsSceneInstaller(_sceneReferences),
            };
            foreach (var currentInstaller in installers)
            {
                _sceneContext.AddNormalInstaller(currentInstaller);
            }

            _sceneContext.Run();
        }
    }
}