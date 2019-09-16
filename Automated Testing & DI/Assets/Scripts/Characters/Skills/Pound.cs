namespace Skills
{
    public struct Pound : ISkill
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
        public float PhysicalDmg => caster.Stats_Total.Strength * 8;
        public float MagicDmg => 0;
        public float Heal => 0;
    }

}
