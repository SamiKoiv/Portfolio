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

        Container.Bind<IItem>().FromMock();
        Container.Bind<IWeapon>().FromMock();
        Container.Bind<IArmor>().FromMock();
        Container.Bind<ICharacter>().FromMock();
    }
}
