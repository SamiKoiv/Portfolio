using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    public float restTime = 3;

    [SerializeField] GameObject heroObject = null;
    [SerializeField] GameObject enemyObject = null;
    [SerializeField] TextMeshProUGUI goldText = null;
    [SerializeField] Image partyHealthMeter = null;
    [SerializeField] Image enemyHealthMeter = null;

    ReactiveProperty<float> partyHealth = new ReactiveProperty<float>();
    ReactiveProperty<float> partyHealthMax = new ReactiveProperty<float>();
    ReactiveProperty<float> enemyHealth = new ReactiveProperty<float>();
    ReactiveProperty<float> enemyHealthMax = new ReactiveProperty<float>();

    ReactiveProperty<ICharacter> enemy = new ReactiveProperty<ICharacter>();
    ReactiveProperty<ICharacter> hero = new ReactiveProperty<ICharacter>();

    ReactiveProperty<int> gold = new ReactiveProperty<int>();

    ISpawnEnemy enemySpawner;
    ISpawnHero heroSpawner;

    ICard[] hand = new ICard[5];

    IDisposable enemyAttack;
    IDisposable heroAttack;

    [Inject]
    void GetInjected(ISpawnEnemy enemySpawner, ISpawnHero heroSpawner)
    {
        this.enemySpawner = enemySpawner;
        this.heroSpawner = heroSpawner;
    }

    private void Start()
    {
        gold.Subscribe(amount => goldText.text = string.Empty + amount);
        gold.Value = 0;

        heroObject.SetActive(false);
        enemyObject.SetActive(false);

        //-----------------------------------------------------------------------------------------------

        partyHealth.Value = 100;
        partyHealthMax.Value = 100;

        partyHealth.Subscribe(health => partyHealthMeter.fillAmount = health / partyHealthMax.Value);

        // Hero Death
        partyHealth.Where(x => x <= 0)
            .Subscribe(_ =>
            {
                Debug.Log("Hero fell in exhaustion and enemy escaped swiftly.");
                StopFight();
                RemoveEnemy();
                SetToRest();
            });

        //-----------------------------------------------------------------------------------------------

        enemyHealth.Value = 100;
        enemyHealthMax.Value = 100;

        enemyHealth.Subscribe(health => enemyHealthMeter.fillAmount = health / enemyHealthMax.Value);

        // Enemy Death
        enemyHealth.Where(x => x <= 0)
            .Subscribe(_ =>
            {
                Debug.Log("Enemy was slain.");
                StopFight();
                gold.Value += enemy.Value.GetReward();
                goldText.text = $"{gold.Value}";
                RemoveEnemy();
                NextEnemy();
                StartFight();
            });

        //-----------------------------------------------------------------------------------------------

        NextHero();
        NextEnemy();
        StartFight();
    }

    void RemoveEnemy()
    {
        enemy.Value = null;
        enemyObject.SetActive(false);
    }

    void NextEnemy()
    {
        enemy.Value = enemySpawner.Next();
        Debug.Log($"Spawned {enemy.Value.GetName()}. HP {enemy.Value.GetHealth()}, Attack {enemy.Value.GetAttack()}, Attack rate {enemy.Value.GetAttackRate()}");

        enemyHealthMax.Value = enemy.Value.GetHealth();
        enemyHealth.Value = enemy.Value.GetHealth();

        enemyObject.SetActive(true);
    }

    void NextHero()
    {
        hero.Value = heroSpawner.Next();
        Debug.Log($"Spawned {hero.Value.GetName()}. HP {hero.Value.GetHealth()}, Attack {hero.Value.GetAttack()}, Attack rate {hero.Value.GetAttackRate()}");

        partyHealthMax.Value = hero.Value.GetHealth();
        partyHealth.Value = hero.Value.GetHealth();

        heroObject.SetActive(true);
    }

    void StartFight()
    {
        heroAttack = Observable.Interval(TimeSpan.FromSeconds(hero.Value.GetAttackRate()))
            .Subscribe(_ =>
            {
                Debug.Log($"{hero.Value.GetName()} deals {hero.Value.GetAttack()} dmg to {enemy.Value.GetName()}.");
                enemyHealth.Value -= hero.Value.GetAttack();
            });

        enemyAttack = Observable.Interval(TimeSpan.FromSeconds(enemy.Value.GetAttackRate()))
            .Subscribe(_ =>
            {
                Debug.Log($"{enemy.Value.GetName()} deals {enemy.Value.GetAttack()} dmg to {hero.Value.GetName()}.");
                partyHealth.Value -= enemy.Value.GetAttack();
            });
    }

    void StopFight()
    {
        heroAttack.Dispose();
        enemyAttack.Dispose();
    }

    void SetToRest()
    {
        // Restore Health
        Observable.Timer(TimeSpan.FromSeconds(restTime))
            .Subscribe(_ =>
            {
                // Health restored
                partyHealth.Value = partyHealthMax.Value;
                heroObject.SetActive(true);
                NextEnemy();
                StartFight();
            });

        heroObject.SetActive(false);
    }
}
