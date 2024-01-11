using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public partial class PlayerShootingSystem : SystemBase {
    public event EventHandler OnShoot;

    protected override void OnCreate() {
        RequireForUpdate<Player>();
    }
    //protected override void OnUpdate() {
    //    if(!Input.GetKey(KeyCode.Space)) {
    //        return;
    //    }
    //    SpawnCubesConfig config = SystemAPI.GetSingleton<SpawnCubesConfig>();
    //    foreach(RefRO<LocalTransform> localTransform in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>()) {
    //        Entity entity = EntityManager.Instantiate(config.cubePrefabEntity);
    //        EntityManager.SetComponentData(entity, new LocalTransform {
    //            Position = localTransform.ValueRO.Position,
    //            Rotation = quaternion.identity,
    //            Scale = 1
    //        });
    //    }

    //}
    float ShootTimer = 1f;
    protected override void OnUpdate() {
        if(Input.GetKeyDown(KeyCode.T)) {
            Entity entity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<stunned>(entity, true);
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            Entity entity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<stunned>(entity, false);
        }

        ShootTimer -= SystemAPI.Time.DeltaTime;
        if (ShootTimer > 0) {
            return;
        }
        
        if (!Input.GetKey(KeyCode.Space)) {
            return;
        }
        ShootTimer = 1f;
        SpawnCubesConfig config = SystemAPI.GetSingleton<SpawnCubesConfig>();

        //EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator);

        foreach ((var localTransform,var entity) in 
            SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<stunned>().WithEntityAccess()) {
            
            Entity spawnedEntity = entityCommandBuffer.Instantiate(config.cubePrefabEntity);

            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform {
                Position = localTransform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1
            });

            OnShoot?.Invoke(entity, EventArgs.Empty);
        }
        
        entityCommandBuffer.Playback(EntityManager);
    }
}
