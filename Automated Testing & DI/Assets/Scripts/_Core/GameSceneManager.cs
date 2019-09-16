using Skills;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameSceneManager : MonoBehaviour
{
    [Header("General")]
    public float restTime = 3;
    [SerializeField] BattleUI battleUI;

    [Header("Hero")]
    [SerializeField] CharacterRecipe heroRecipe = null;

    [Header("Enemy")]
    [SerializeField] CharacterRecipe enemyRecipe = null;
    

    ICharacterFactory characterFactory;

    FightController fightController = new FightController();

    ICharacter enemy;
    ICharacter hero;

    ReactiveProperty<int> gold = new ReactiveProperty<int>();

    IReadOnlyReactiveProperty<int> heroHealth;
    IReadOnlyReactiveProperty<int> enemyHealth;

    
    IDisposable heroDeathSub;

    IDisposable enemyHealthMeterSub;
    IDisposable enemyDeathSub;
    IDisposable restSub;

    [Inject]
    void GetInjected(ICharacterFactory characterFactory)
    {
        this.characterFactory = characterFactory;
    }

    private void Start()
    {

        gold.Subscribe(amount => battleUI.SetGold(gold.Value));
        gold.Value = 0;

        NextHero();
        NextEnemy();
        fightController.SetFight(hero, enemy);
        fightController.StartFight();
    }

    private void OnDestroy()
    {
        heroDeathSub.Dispose();
        enemyDeathSub.Dispose();

        if (restSub != null)
            restSub.Dispose();
    }

    void NextHero()
    {
        hero = characterFactory.Next(heroRecipe);
        Debug.Log($"Spawned {hero.Name}. HP {hero.Stats_Total.MaxHp}, Attack {hero.Stats_Total.Strength}, Attack rate {hero.Stats_Total.AttackRate}");

        battleUI.SetHero(hero);
        battleUI.HeroAppears();

        heroDeathSub = hero.CurrentHP
            .Where(x => x <= 0)
            .Subscribe(_ =>
            {
                Debug.Log($"{hero.Name} fell in exhaustion. Enemy escaped.");
                fightController.StopFight();
                battleUI.HeroFell();
                battleUI.EnemyEscaped();
                Rest();
            });

    }

    void NextEnemy()
    {
        enemy = characterFactory.Next(enemyRecipe);
        Debug.Log($"Spawned {enemy.Name}. HP {enemy.Stats_Total.MaxHp}, Attack {enemy.Stats_Total.Strength}, Attack rate {enemy.Stats_Total.AttackRate}");

        battleUI.SetEnemy(enemy);
        battleUI.EnemyAppears();

        enemyDeathSub = enemy.CurrentHP
            .Where(x => x <= 0)
            .Subscribe(_ =>
            {
                Debug.Log("Enemy was slain.");
                fightController.StopFight();
                battleUI.EnemyFell();
                gold.Value += enemy.RewardGold;

                NextEnemy();
                fightController.SetFight(hero, enemy);
                fightController.StartFight();
            });
    }

    void Rest()
    {
        // Restore Health
        restSub = Observable.Timer(TimeSpan.FromSeconds(restTime))
            .Subscribe(_ =>
            {
                // Health restored
                ISkill restoration = new FullRestore();
                restoration.Initialize(null, hero);
                hero.ApplySkill(restoration);

                battleUI.HeroRestored();

                NextEnemy();
                fightController.SetFight(hero, enemy);
                fightController.StartFight();
            });
    }
}
