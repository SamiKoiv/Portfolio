using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IGetName, ISetName
{
    protected string _name;

    public void SetItem(string name)
    {
        _name = name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }
}
