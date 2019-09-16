using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UniRx;

public class BattleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText = null;

    [Header("Hero")]
    ICharacter hero;
    [SerializeField] GameObject heroObject = null;
    [SerializeField] TextMeshProUGUI heroName = null;
    [SerializeField] Slider heroHealthMeter = null;
    [SerializeField] TextMeshProUGUI heroHealthText = null;

    [Header("Enemy")]
    ICharacter enemy;
    [SerializeField] GameObject enemyObject = null;
    [SerializeField] TextMeshProUGUI enemyName = null;
    [SerializeField] Slider enemyHealthMeter = null;
    [SerializeField] TextMeshProUGUI enemyHealthText = null;

    IDisposable subHeroHp;
    IDisposable subEnemyHp;

    private void Start()
    {
        heroObject.SetActive(false);
        enemyObject.SetActive(false);
    }

    private void OnDestroy()
    {
        subHeroHp.Dispose();
        subEnemyHp.Dispose();
    }

    public void SetGold(int amount) => goldText.text = string.Empty + amount;


    public void SetHero(ICharacter hero)
    {
        this.hero = hero;
        subHeroHp = hero.CurrentHP.Subscribe(hp => SetHeroHealth(hp, hero.Stats_Total.MaxHp));
    }
    public void SetEnemy(ICharacter enemy)
    {
        this.enemy = enemy;
        subEnemyHp = enemy.CurrentHP.Subscribe(hp => SetEnemyHealth(hp, enemy.Stats_Total.MaxHp));
    }


    public void SetHeroHealth(float hp, float maxHp)
    {
        heroHealthMeter.value = (float)hp / maxHp;
        heroHealthText.text = $"HP: {hp} / {maxHp}";
    }

    public void SetEnemyHealth(float hp, float maxHp)
    {
        enemyHealthMeter.value = (float)hp / maxHp;
        enemyHealthText.text = $"HP: {hp} / {maxHp}";
    }

    public void HeroAppears()
    {
        heroObject.SetActive(true);
        heroName.enabled = true;
        heroName.text = hero.Name;
    }

    public void HeroFell()
    {
        heroObject.SetActive(false);
    }

    public void HeroRestored()
    {
        heroObject.SetActive(true);
    }

    public void EnemyAppears()
    {
        enemyObject.SetActive(true);
        enemyName.enabled = true;
        enemyName.text = enemy.Name;
    }

    public void EnemyEscaped()
    {
        enemyObject.SetActive(false);
        enemyName.enabled = false;
    }

    public void EnemyFell()
    {
        enemyObject.SetActive(false);
    }
}
