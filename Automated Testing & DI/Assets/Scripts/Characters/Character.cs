using System;
using UnityEngine;
using UniRx;

public class Character: ICharacter
{
    string characterName;

    Stats stats_base;

    ReactiveProperty<Stats> stats_gear = new ReactiveProperty<Stats>();
    ReactiveProperty<Stats> stats_temp = new ReactiveProperty<Stats>();
    ReactiveProperty<Stats> stats_total = new ReactiveProperty<Stats>();

    IDisposable gearSub;
    IDisposable tempSub;

    ReactiveProperty<int> currentHp = new ReactiveProperty<int>();
    int reward = 5;

    public Character(string name, Stats stats)
    {
        characterName = name;
        stats_base = stats;
        currentHp.Value = Stats_Base.MaxHp;

        gearSub = stats_gear.Subscribe(_ => BuildTotalStats());
        tempSub = stats_temp.Subscribe(_ => BuildTotalStats());
    }

    ~Character()
    {
        gearSub.Dispose();
        tempSub.Dispose();
    }

    public string Name => characterName;
    public Stats Stats_Base => stats_base;
    public Stats Stats_Gear => stats_gear.Value;
    public Stats Stats_Temp => stats_temp.Value;
    public Stats Stats_Total => stats_total.Value;
    ReactiveProperty<int> ICharacter.CurrentHP => currentHp;
    public int RewardGold => reward;

    public void ApplySkill(ISkill skill)
    {
        // DAMAGE

        // 1. REDUCE with corresponding defence.
        // 2. RESIST with corresponding resistance.
        // 3. APPLY to Hp.

        float totalDmg = 0;

        float physicalDefended = skill.PhysicalDmg - Stats_Total.PhysicalDefence;
        float physicalResisted = physicalDefended - physicalDefended * Stats_Total.PhysicalResistance;
        float magicDefended = skill.MagicDmg - Stats_Total.MagicDefence;
        float magicResisted = magicDefended - magicDefended * Stats_Total.MagicResistance;
        totalDmg = physicalResisted + magicResisted;

        if (totalDmg > 0)
        {
            Debug.Log($"{skill.Caster.Name} deals {totalDmg} dmg to {Name}.");
            currentHp.Value = Mathf.FloorToInt(currentHp.Value - totalDmg);

            // Life Leech effect
            float lifeLeechHp = skill.Caster.Stats_Total.LifeLeech * totalDmg;

            if (skill.Caster.Stats_Total.LifeLeech > 0)
            {
                Debug.Log($"{skill.Caster.Name} drained {lifeLeechHp} hp.");
                skill.Caster.CurrentHP.Value += Mathf.FloorToInt(totalDmg * skill.Caster.Stats_Total.LifeLeech);
            }
        }

        // HEAL

        if (skill.Heal > 0)
        currentHp.Value = Mathf.Min(Stats_Total.MaxHp, Mathf.FloorToInt(currentHp.Value + skill.Heal));
    }

    void BuildTotalStats()
    {
        stats_total.Value = stats_base.Add(stats_gear.Value).Add(stats_temp.Value);
    }
}
