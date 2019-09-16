using Zenject;
using NUnit.Framework;
using UniRx;

[TestFixture]
public class TestCharacter : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        TestInstaller.Install(Container);
    }

    [Test]
    public void Injection()
    {
        ICharacter character = Container.Resolve<ICharacter>();
        Assert.That(character != null);
    }

    [Test]
    public void HpMatchMaxHp()
    {
        ICharacter character = Container.Resolve<ICharacter>();
        //Debug.Log($"HP: {character.GetHealthReactive().Value} / {character.GetMaxHealth()}");
        Assert.That(character.CurrentHP.Value == character.Stats_Total.MaxHp);
    }

    [Test]
    public void TestHealthSubscription_Reactive()
    {
        ICharacter character = Container.Resolve<ICharacter>();

        ReactiveProperty<int> hp = character.CurrentHP;
        IReadOnlyReactiveProperty<bool> changed = hp.Select(x => x != character.Stats_Total.MaxHp).ToReactiveProperty();
        Assert.That(changed.Value == false);

        character.CurrentHP.Value -= 5;
        Assert.That(changed.Value == true);
    }
}