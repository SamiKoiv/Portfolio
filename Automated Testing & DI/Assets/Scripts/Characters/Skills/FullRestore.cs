namespace Skills
{
    public class FullRestore : ISkill
    {
        ICharacter caster;
        ICharacter target;

        public void Initialize(ICharacter caster, ICharacter target)
        {
            this.caster = caster;
            this.target = target;
        }

        public ICharacter Caster => caster;
        public ICharacter Target => target;

        public float PhysicalDmg => 0;
        public float MagicDmg => 0;
        public float Heal => Target.Stats_Total.MaxHp;

    }
}
