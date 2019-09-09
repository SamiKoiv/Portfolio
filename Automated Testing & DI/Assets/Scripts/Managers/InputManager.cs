using UnityEngine;
using UniRx;
using Zenject;

public class InputManager : IInitializable
{
    public static float MoveX { get; private set; }
    public static float MoveY { get; private set; }

    public void Initialize()
    {
        //--------------------------------------------------------------------------
        // MAIN
        var updateStream = Observable.EveryUpdate()
            .Where(_ => !GameManager.Loading.Value);

        GameManager.Loading.Subscribe(_ => ResetInput());

        //--------------------------------------------------------------------------
        // MENU

        //var menuStream = updateStream
        //    .Where(_ => GameManager.InMenu.Value == true);

        //--------------------------------------------------------------------------
        // GAMEPLAY
        var gameplayStream = updateStream
            .Where(_ => GameManager.InMenu.Value == false);

        var moveX = gameplayStream
            .Select(x => Input.GetAxis("Horizontal"))
            .Subscribe(x => MoveX = x);

        var moveY = gameplayStream
            .Select(y => Input.GetAxis("Vertical"))
            .Subscribe(y => MoveY = y);

        //--------------------------------------------------------------------------
        // DEBUG

        //var debug = gameplayStream
        //    .Subscribe(_ => Debug.Log($"X: {MoveX}, Y: {MoveY}"));
    }

    void ResetInput()
    {
        MoveX = 0;
        MoveY = 0;
    }

}
