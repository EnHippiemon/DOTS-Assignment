using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[BurstCompile]
public class SpawnerAuthoring : MonoBehaviour
{
    [FormerlySerializedAs("Prefab")] public GameObject prefab;
    [FormerlySerializedAs("SpawnRate")] public float spawnRate;

    [BurstCompile]
    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        [BurstCompile]
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = float2.zero,
                NextSpawnTime = 0,
                SpawnRate = authoring.spawnRate
            });
        }
    }
}