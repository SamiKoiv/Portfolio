using UnityEngine;

[CreateAssetMenu]
public class CharacterProfile : ScriptableObject
{
    public string CharacterName;
    public bool invulnerable;
    public float MaxHP;

    public float Attack;
    public float Defence;
}
