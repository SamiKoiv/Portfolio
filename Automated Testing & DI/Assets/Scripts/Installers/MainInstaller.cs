using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MainInstaller", menuName = "Installers/MainInstaller")]
public class MainInstaller : ScriptableObjectInstaller<MainInstaller>
{
    [SerializeField] ItemDatabase itemDatabase;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<IInventory>().To<InventoryByID>().AsTransient();
    }
}