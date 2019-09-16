public interface IFightController
{
    void SetFight(ICharacter hero, ICharacter enemy);
    void StartFight();
    void StopFight();
}
