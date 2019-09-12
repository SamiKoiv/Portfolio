using UnityEngine;

public class SimpleHeroSpawner : ISpawnHero
{
    Color colorRandom => new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
    int healthRandom => Random.Range(50, 100);
    int attackRandom => Random.Range(10, 25);
    float attackRateRandom => Random.Range(0.1f, 3f);

    public ICharacter Next()
    {
        SimpleCharacter character = new SimpleCharacter
        {
            Name = "Knight",
            Health = healthRandom,
            Attack = attackRandom,
            AttackRate = attackRateRandom,
            EnemyColor = colorRandom
        };

        character.CalculateReward();

        return character;
    }
}
