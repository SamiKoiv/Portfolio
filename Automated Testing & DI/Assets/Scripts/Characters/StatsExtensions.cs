    public static class StatsExtensions
    {
        public static Stats Add(this Stats old, Stats increment)
        {
            return new Stats
            {
                // Value + Value
                Vitality = old.Vitality + increment.Vitality,
                Strength = old.Strength + increment.Strength,
                Dexterity = old.Dexterity + increment.Dexterity,
                Intelligence = old.Intelligence + increment.Intelligence,

                PhysicalDefence = old.PhysicalDefence + increment.PhysicalDefence,
                PhysicalResistance = old.PhysicalResistance + increment.PhysicalResistance,
                MagicDefence = old.MagicDefence + increment.MagicDefence,
                MagicResistance = old.MagicResistance + old.MagicResistance * increment.MagicResistance,

                AttackSpeed = old.AttackSpeed + increment.AttackSpeed,
                CritChance = old.CritChance + increment.CritChance,
                CritDamage = old.CritDamage + increment.CritDamage,
                LifeLeech = old.LifeLeech + increment.LifeLeech
            };
        }
    }
