using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestTarget : MonoBehaviour
{
    public string description;

    public UnityEvent ProgressEvent;

    public void Complete()
    {

    }

    public void Progress()
    {
        ProgressEvent.Invoke();
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

}
