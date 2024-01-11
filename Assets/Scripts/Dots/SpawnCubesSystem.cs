using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public partial class SpawnCubesSystem : SystemBase{
    protected override void OnCreate() {
        RequireForUpdate<SpawnCubesConfig>();
    }

    protected override void OnUpdate() {
        this.Enabled = false;
        SpawnCubesConfig config = SystemAPI.GetSingleton<SpawnCubesConfig>();
        for(int i = 0; i < config.amountToSpawn; i ++) {
            Entity entity = EntityManager.Instantiate(config.cubePrefabEntity);
            EntityManager.SetComponentData(entity, new LocalTransform {
                Position = new float3(UnityEngine.Random.Range(-01f, +5f), 0.6f, UnityEngine.Random.Range(-4f, +7f)),
                Rotation = quaternion.identity,
                Scale = 1
            });
        }
    }
}
