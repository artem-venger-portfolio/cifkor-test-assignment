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

            var installers = new InstallerBase[]
            {
                new TabsSceneInstaller(_sceneReferences),
            };
            foreach (var currentInstaller in installers)
            {
                _sceneContext.AddNormalInstaller(currentInstaller);
            }

            _sceneContext.Run();

            var navigationPanelViewFromContainer = _sceneContext.Container.Resolve<INavigationPanelView>();
            navigationPanelViewFromContainer.CreateTabs();
        }
    }
}