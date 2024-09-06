using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct EnemyMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;

        new EnemyMoveJob
        {
            DeltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
[StructLayout(LayoutKind.Auto)]
public partial struct EnemyMoveJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    private void Execute(ref LocalTransform transform, EnemyMoveSpeed speed)
    {
        transform.Position.xy -= new float2(0, 1) * DeltaTime * speed.Value;
    }
}