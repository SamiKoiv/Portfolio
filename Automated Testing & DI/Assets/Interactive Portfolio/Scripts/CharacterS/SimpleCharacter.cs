using UnityEngine;

public class SimpleCharacter : ICharacter
{
    public string Name;
    public int Health;
    public int Attack;
    public float AttackRate;
    public Color EnemyColor;
    public int Reward;

    public string GetName() => Name;
    public float GetAttack() => Attack;
    public float GetAttackRate() => AttackRate;
    public float GetHealth() => Health;
    public int GetReward() => Reward;

    public void CalculateReward()
    {
        Reward = Mathf.FloorToInt(Attack * Health * 0.01f);
    }
}
