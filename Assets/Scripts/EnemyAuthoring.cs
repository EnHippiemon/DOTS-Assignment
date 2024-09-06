using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float enemySpeed;
    
    public class EnemyAuthoringBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyMoveSpeed { Value = authoring.enemySpeed });
        }
    }
}

public struct EnemyMoveSpeed : IComponentData
{
    public float2 Value;
}