using UnityEngine;

[CreateAssetMenu(menuName = "Global Variables/String (ReadOnly)")]
public class String_ReadOnly : ScriptableObject
{
    [SerializeField] string value;

    public string Value
    {
        get
        {
            return value;
        }
    }
}
