using System;
using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;

[Serializable]
public struct ECS_RotationSpeed : IComponentData
{
    public float3 Axis;
    public float RadiansPerSecond;
}
