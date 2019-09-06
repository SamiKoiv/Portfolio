using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static float MoveX { get; private set; }
    public static float MoveY { get; private set; }

    // TODO: Make InputManager react to states like InMenu etc.
    void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetAxis("Horizontal") != MoveX)
            .Subscribe(_ => MoveX = Input.GetAxis("Horizontal"));

        Observable.EveryUpdate()
            .Where(_ => Input.GetAxis("Vertical") != MoveY)
            .Subscribe(_ => MoveY = Input.GetAxis("Vertical"));
    }
    
}
