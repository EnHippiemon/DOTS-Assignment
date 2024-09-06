using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerAuthoring : MonoBehaviour
{
    [FormerlySerializedAs("Prefab")] public GameObject prefab;
    [FormerlySerializedAs("SpawnRate")] public float spawnRate;

    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
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