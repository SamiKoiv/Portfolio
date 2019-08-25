using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Global Variables/AnimationCurve (ReadOnly)")]
public class AnimationCurve_ReadOnly : ScriptableObject
{
    [SerializeField] AnimationCurve curve;

    public AnimationCurve Curve
    {
        get
        {
            return curve;
        }
    }
}
