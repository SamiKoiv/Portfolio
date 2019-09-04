using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Mocks;

public class TestInstaller : Installer<TestInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle().NonLazy();
        Container.Bind<IInventory>().To<InventoryByID>().AsTransient();

        Container.Bind<Item>().To<MockItem>().AsTransient();
    }
}
