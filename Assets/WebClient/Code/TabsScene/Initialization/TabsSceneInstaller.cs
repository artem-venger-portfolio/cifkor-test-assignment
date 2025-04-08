using Zenject;

namespace WebClient
{
    public class TabsSceneInstaller : InstallerBase
    {
        public override void InstallBindings()
        {
            Container.Bind<MonoBehaviourFunctions>()
                     .FromNewComponentOnNewGameObject()
                     .WithGameObjectName(nameof(MonoBehaviourFunctions))
                     .AsSingle()
                     .NonLazy();
        }
    }
}