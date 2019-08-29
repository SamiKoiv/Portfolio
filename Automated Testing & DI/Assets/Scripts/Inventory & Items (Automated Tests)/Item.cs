using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject, IGetName
{
    string _name;

    public void SetItem(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }
}
