using UnityEngine;

public abstract class Tag : ScriptableObject
{
    [SerializeField] string tagName;

    public string GetName()
    {
        return tagName;
    }
}
