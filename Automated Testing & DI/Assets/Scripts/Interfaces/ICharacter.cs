using UniRx;

public interface ICharacter
{
    string Name { get; }
    Stats Stats_Base { get; }
    Stats Stats_Gear { get; }
    Stats Stats_Temp { get; }
    Stats Stats_Total { get; }
    ReactiveProperty<int> CurrentHP { get; }
    int RewardGold { get; }

    void ApplySkill(ISkill skill);
}
