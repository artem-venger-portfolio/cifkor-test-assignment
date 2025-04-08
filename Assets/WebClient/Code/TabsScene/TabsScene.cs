using UnityEngine;

namespace WebClient
{
    public class TabsScene : MonoBehaviour
    {
        [SerializeField]
        private TabsSceneReferences _sceneReferences;

        private void Start()
        {
            var initialization = new TabsSceneInitialization(_sceneReferences);
            initialization.Perform();
        }
    }
}