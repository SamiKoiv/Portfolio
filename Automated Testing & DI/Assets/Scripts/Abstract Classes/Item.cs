using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IGetName, ISetName, IGetID
{
    protected string m_name;
    protected int m_id;

    public void SetItem(string name)
    {
        m_name = name;
    }

    public void SetName(string name)
    {
        m_name = name;
    }

    public string GetName()
    {
        return m_name;
    }

    public int GetID()
    {
        return m_id;
    }
}
