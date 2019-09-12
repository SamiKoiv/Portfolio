using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using UniRx;

public class SceneLoader
{
    [SerializeField] HandleLibrary handleLibrary = new HandleLibrary();

    ZenjectSceneLoader zenLoader;

    [System.Serializable]
    public class HandleLibrary
    {
        public readonly int FrontPage = 1;
        public readonly int NavigationBar = 2;
        public readonly int CharacterPage = 3;
        public readonly int SkillsPage = 4;
        public readonly int IdleGame = 5;
    }

    Dictionary<SceneID, int> handleSearch = new Dictionary<SceneID, int>();

    ReactiveProperty<int> loadCount = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<bool> Loading { get; private set; }

    [Inject]
    void GetReference(ZenjectSceneLoader loader)
    {
        zenLoader = loader;
    }

    public SceneLoader()
    {
        Loading = loadCount.Select(x => x > 0).ToReactiveProperty();

        handleSearch.Add(SceneID.FrontPage, handleLibrary.FrontPage);
        handleSearch.Add(SceneID.NavigationBar, handleLibrary.NavigationBar);
        handleSearch.Add(SceneID.CharacterPage, handleLibrary.CharacterPage);
        handleSearch.Add(SceneID.SkillsPage, handleLibrary.SkillsPage);
        handleSearch.Add(SceneID.IdleGame, handleLibrary.IdleGame);
    }

    public void SwitchScene(int previous, int next)
    {
        // TODO

        
    }

    public void LoadSceneAdditive(SceneID scene)
    {
        int handle;

        if (handleSearch.TryGetValue(scene, out handle))
            zenLoader.LoadSceneAsync(handle, LoadSceneMode.Additive);
        else
            Debug.Log($"Scene Loader: Error loading scene: {scene}");
    }

    public void UnloadScene(SceneID scene)
    {
        int handle;

        if (handleSearch.TryGetValue(scene, out handle))
            SceneManager.UnloadSceneAsync(handle);
        else
            Debug.Log($"Scene Loader: Error unloading scene: {scene}");
    }

    public void LoadingState()
    {
        loadCount.Value += 1;
    }
}
