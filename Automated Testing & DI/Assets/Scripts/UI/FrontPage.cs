using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class FrontPage : MonoBehaviour
{
    [SerializeField] Button button = null;

    SceneLoader sceneLoader;
    
    [Inject]
    void GetReferences(SceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }

    void Start()
    {
        button.OnClickAsObservable().Subscribe(_ => CloseFrontPage());
    }

    void CloseFrontPage()
    {
        sceneLoader.LoadSceneAdditive(SceneID.NavigationBar);
        sceneLoader.UnloadScene(SceneID.FrontPage);
    }
}
