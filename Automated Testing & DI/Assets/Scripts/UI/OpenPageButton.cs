using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class OpenPageButton : MonoBehaviour
{
    [SerializeField] Button button = null;
    [SerializeField] SceneID pageID = SceneID.Empty;
    [SerializeField] bool loadOnStart = false;

    static SceneID currentScene = SceneID.Empty;

    SceneLoader sceneLoader;

    [Inject]
    void GetReferences(SceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }

    private void Start()
    {
        button.OnPointerClickAsObservable()
            .Subscribe(_ => LoadScene());

        if (loadOnStart)
            LoadScene();
    }

    public void LoadScene()
    {
        if (currentScene == pageID)
            return;

        UnloadPrevious();
        sceneLoader.LoadSceneAdditive(pageID);
        currentScene = pageID;
    }

    void UnloadPrevious()
    {
        if (currentScene != SceneID.Empty)
            sceneLoader.UnloadScene(currentScene);
    }
}
