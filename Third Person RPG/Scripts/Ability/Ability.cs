using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;

public class Ability : ScriptableObject
{
    [Header("General")]
    public string abilityName;
    public Type type;
    public Branch family;

    [Header("Hit Damage / Heal")]
    public Targets targets;
    public float maxDamage;
    public float maxDistance;
    public AnimationCurve dmgFallDistance;
    public float maxRadius;
    public AnimationCurve dmgFallRadius;
}
