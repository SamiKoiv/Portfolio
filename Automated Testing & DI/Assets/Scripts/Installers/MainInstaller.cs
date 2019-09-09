using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MainInstaller", menuName = "Installers/MainInstaller")]
public class MainInstaller : ScriptableObjectInstaller<MainInstaller>
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
    }
}