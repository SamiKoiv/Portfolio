using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Function
{

    List<Action> _actions = new List<Action>();

    public void Subscribe(Action action)
    {
        _actions.Add(action);
    }

    public void Unsubscribe(Action action)
    {
        _actions.Remove(action);
    }

    public void Invoke()
    {
        for (int i = _actions.Count - 1; i >= 0; i--)
            _actions[i].Invoke();
    }

    public void Clear()
    {
        _actions.Clear();
    }

}
