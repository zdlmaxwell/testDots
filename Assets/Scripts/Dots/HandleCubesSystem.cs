using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct HandleCubesSystem : ISystem{

    public void OnUpdate(ref SystemState state) {
        foreach (RotatingMovingCubeAspect rotatingMovingCubeAspect in
            SystemAPI.Query<RotatingMovingCubeAspect>().WithAll<RotateCube>()) {
            rotatingMovingCubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
        }

        //RotatingCubeJob job = new RotatingCubeJob {
        //    deltaTime = SystemAPI.Time.DeltaTime
        //};
        //job.ScheduleParallel();
    }

    [BurstCompile]
    [WithAll(typeof(RotateCube))]
    public partial struct RotatingCubeJob : IJobEntity {

        public float deltaTime;
        public void Execute(  RotatingMovingCubeAspect rotatingMovingCubeAspect) {
            rotatingMovingCubeAspect.MoveAndRotate(deltaTime);
        }
    }
}
