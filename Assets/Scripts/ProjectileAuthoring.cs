using Unity.Burst;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public class ProjectileAuthoring : MonoBehaviour
{
    public float projectileSpeed;

    [BurstCompile]
    public class ProjectileAuthoringBaker : Baker<ProjectileAuthoring>
    {
        [BurstCompile]
        public override void Bake(ProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ProjectileMoveSpeed { Value = authoring.projectileSpeed });
        }
    }
}