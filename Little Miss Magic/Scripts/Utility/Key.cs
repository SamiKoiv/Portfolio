using UnityEngine;

public class Key : MonoBehaviour, IKey
{
    public string m_Key;

    public string GetKey()
    {
        return m_Key;
    }
}
