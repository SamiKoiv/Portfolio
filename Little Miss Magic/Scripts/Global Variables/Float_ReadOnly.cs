using UnityEngine;

[CreateAssetMenu(menuName = "Global Variables/Float (ReadOnly)")]
public class Float_ReadOnly : ScriptableObject
{
    [SerializeField] float value;

    public float Value
    {
        get
        {
            return value;
        }
    }
}
