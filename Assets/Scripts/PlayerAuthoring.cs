using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public class PlayerAuthoring : MonoBehaviour
{
    public float moveSpeed;

    public GameObject projectilePrefab;

    [BurstCompile]
    private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        [BurstCompile]
        public override void Bake(PlayerAuthoring authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);
            
            AddComponent(playerEntity, new PlayerMoveSpeed
            {
                Value = authoring.moveSpeed
            });

            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);
            
            AddComponent(playerEntity, new ProjectilePrefab
            {
                Value = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}

[BurstCompile]
public struct PlayerMoveInput : IComponentData
{
    public float2 Value;
}

[BurstCompile]
public struct PlayerMoveSpeed : IComponentData
{
    public float Value;
}

[BurstCompile]
public struct PlayerTag : IComponentData { }

[BurstCompile]
public struct ProjectilePrefab : IComponentData
{
    public Entity Value;
}

[BurstCompile]
public struct ProjectileMoveSpeed : IComponentData
{
    public float Value;
}

[BurstCompile]
public struct FireProjectileTag : IComponentData, IEnableableComponent { }