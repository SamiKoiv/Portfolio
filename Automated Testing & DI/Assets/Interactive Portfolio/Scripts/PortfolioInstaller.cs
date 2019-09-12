using Zenject;

public class PortfolioInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<PortfolioManager>().AsSingle().NonLazy();

        Container.Bind<ISpawnHero>().To<SimpleHeroSpawner>().AsSingle();
        Container.Bind<ISpawnEnemy>().To<SimpleEnemySpawner>().AsSingle();
    }

}