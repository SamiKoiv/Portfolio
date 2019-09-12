using UnityEngine;

public class SimpleEnemySpawner : ISpawnEnemy
{
    Color colorRandom => new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    int healthRandom => Random.Range(50, 100);
    int attackRandom => Random.Range(1, 20);
    float attackRateRandom => Random.Range(0.1f, 5f);

    public ICharacter Next()
    {
        SimpleCharacter character = new SimpleCharacter
        {
            Name = "Goblin",
            Health = healthRandom,
            Attack = attackRandom,
            AttackRate = attackRateRandom,
            EnemyColor = colorRandom
        };

        character.CalculateReward();

        return character;
    }
}
