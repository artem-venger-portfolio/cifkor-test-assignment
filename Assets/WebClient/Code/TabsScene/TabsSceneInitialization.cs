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
        }
    }
}