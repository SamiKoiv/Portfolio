using Zenject;

public class TestInstaller : Installer<TestInstaller>
{
    public override void InstallBindings()
    {
        MainInstaller.InstallFromResource("MainInstaller", Container);

        Container.Bind<IItem>().FromMock();
        Container.Bind<IWeapon>().FromMock();
        Container.Bind<IArmor>().FromMock();

        Container.Bind<ICharacter>()
            .FromInstance(new Character("Test Dummy", dummyStats))
            .AsTransient();

        Container.Bind<Stats>()
            .FromInstance(dummyStats)
            .AsTransient();
    }

    Stats dummyStats = new Stats
    {
        Vitality = 5,
        Strength = 5,
        Dexterity = 5,
        Intelligence = 5,
        AttackSpeed = 1
    };
}
