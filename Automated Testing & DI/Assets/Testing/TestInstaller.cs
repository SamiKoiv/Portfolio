using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Moq;

public class TestInstaller : Installer<TestInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameParameters>()
            .FromScriptableObjectResource("GameParameters")
            .AsSingle();

        Container.Bind<ItemDatabase>()
            .FromScriptableObjectResource("ItemDatabase")
            .AsSingle();

        Container.BindInterfacesAndSelfTo<GameManager>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<InputManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IInventory>()
            .To<InventoryByID>()
            .AsTransient();

        Container.BindInstance(new Mock<IItem>().Object);
        Container.BindInstance(new Mock<IWeapon>().Object);
        Container.BindInstance(new Mock<IArmor>().Object);
        Container.BindInstance(new Mock<ICharacter>().Object);
    }
}
