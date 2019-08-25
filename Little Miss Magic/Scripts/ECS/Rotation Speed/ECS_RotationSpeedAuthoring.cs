using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ECS_RotationSpeedAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float3 Axis;
    public float DegreesPerSecond;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var data = new ECS_RotationSpeed { Axis = this.Axis, RadiansPerSecond = math.radians(DegreesPerSecond) };
        dstManager.AddComponentData(entity, data);
    }
}
