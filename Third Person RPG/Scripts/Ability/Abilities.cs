using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{

    public enum Type
    {
        Passive,
        Active
    };

    public enum Branch
    {
        Internal,
        External,
        Radical,
        Special
    };

    public enum Targets
    {
        Single,
        Multiple
    };

    public enum CombatEffect
    {
        None,
        Damage,
        Heal,
        Buff,
        Debuff,
        Special
    };

    public enum Target
    {
        None,
        Self,
        Enemy,
        Allies
    };

    public enum PermanentEffect
    {
        None
    };



}
