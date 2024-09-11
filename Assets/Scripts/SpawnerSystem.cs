using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[StructLayout(LayoutKind.Auto)]
public partial struct SpawnerSystem : ISystem
{
    private float _entityInWave;

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                _entityInWave = 0;
            }
            
            if (_entityInWave > 30) return;
            var newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            // Adding x position to each new entity 
            var position = new float3(spawner.ValueRO.SpawnPosition.x + _entityInWave/2 - 7.5f, spawner.ValueRO.SpawnPosition.y + 5, 0);
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPositionRotationScale(position, quaternion.identity, 0.5f));
            spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            _entityInWave++;
        }
    }
}