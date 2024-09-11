using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public class EnemyAuthoring : MonoBehaviour
{
    public float enemySpeed;
    
    [BurstCompile]
    public class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        [BurstCompile]
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyMoveSpeed { Value = authoring.enemySpeed });
        }
    }
}

[BurstCompile]
public struct EnemyMoveSpeed : IComponentData
{
    public float2 Value;
}