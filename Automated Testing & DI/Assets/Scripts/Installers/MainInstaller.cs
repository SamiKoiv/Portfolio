using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MainInstaller", menuName = "Installers/MainInstaller")]
public class MainInstaller : ScriptableObjectInstaller<MainInstaller>
{
    [SerializeField] ItemDatabase itemDatabase;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<InputManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IInventory>()
            .To<InventoryByID>()
            .AsTransient();

        Container.Bind<InputManager>()
            .AsSingle();
    }
}