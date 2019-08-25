using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ECS_RotationSpeedSystem : JobComponentSystem
{
    [BurstCompile]
    struct RotationJob : IJobForEach<Rotation, ECS_RotationSpeed>
    {
        public float DeltaTime;

        public void Execute(ref Rotation rotation, [ReadOnly] ref ECS_RotationSpeed rotationSpeed)
        {
            rotation.Value = math.mul(math.normalize(rotation.Value), quaternion.AxisAngle(math.normalize(rotationSpeed.Axis), rotationSpeed.RadiansPerSecond * DeltaTime));
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new RotationJob
        {
            DeltaTime = Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }
}
