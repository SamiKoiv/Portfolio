public interface ISkill
{
    void Initialize(ICharacter caster, ICharacter target);
    ICharacter Caster { get; }
    ICharacter Target { get; }
    float PhysicalDmg { get; }
    float MagicDmg { get; }
    float Heal { get; }
}
