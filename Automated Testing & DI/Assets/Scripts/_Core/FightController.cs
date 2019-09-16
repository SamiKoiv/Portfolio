using System;
using UniRx;
using Skills;

public class FightController : IFightController
{
    ICharacter hero;
    ICharacter enemy;

    IDisposable heroAttack;
    IDisposable enemyAttack;

    ~FightController()
    {
        StopFight();
    }

    public void SetFight(ICharacter hero, ICharacter enemy)
    {
        this.hero = hero;
        this.enemy = enemy;
    }

    public void StartFight()
    {
        heroAttack = Observable.Interval(TimeSpan.FromSeconds(hero.Stats_Total.AttackRate))
            .Subscribe(_ =>
            {
                ISkill pound = new Pound();
                pound.Initialize(hero, enemy);
                enemy.ApplySkill(pound);
            });

        enemyAttack = Observable.Interval(TimeSpan.FromSeconds(enemy.Stats_Total.AttackRate))
            .Subscribe(_ =>
            {
                ISkill pound = new Pound();
                pound.Initialize(enemy, hero);
                hero.ApplySkill(pound);
            });
    }

    public void StopFight()
    {
        heroAttack.Dispose();
        enemyAttack.Dispose();
    }

}
