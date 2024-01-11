using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct RotatingCuteSystem : ISystem {

    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<RotateSpeed>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        state.Enabled = false;
        return;
        //foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed)
        //        in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotateCube>()) {

        //    localTransform.ValueRW = localTransform.ValueRW.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);

        //}
        RotatingCubeJob job = new RotatingCubeJob {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        job.ScheduleParallel();
    }

    [BurstCompile]
    [WithAll(typeof(RotateCube))]
    public partial struct RotatingCubeJob : IJobEntity {

        public float deltaTime;
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed) {
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime );
        }
    }
}
