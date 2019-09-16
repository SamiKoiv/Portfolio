using UnityEngine;

[CreateAssetMenu(menuName = "Game Entities/Character")]
public class CharacterRecipe : ScriptableObject
{
    [SerializeField] string m_name = string.Empty;
    [SerializeField] Stats m_stats = new Stats();

    public string GetName() => m_name;
    public Stats GetStats() => m_stats;
}
