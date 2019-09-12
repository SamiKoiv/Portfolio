using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PortfolioManager : IInitializable
{
    SceneID frontPage = SceneID.FrontPage;
    SceneLoader sceneLoader;

    [Inject]
    void GetReferences(SceneLoader sceneLoader)
    {
        this.sceneLoader = sceneLoader;
    }

    public void Initialize()
    {
        sceneLoader.LoadSceneAdditive(frontPage);
    }
}
