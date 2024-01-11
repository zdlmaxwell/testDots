using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotateCubeAuthoring : MonoBehaviour{

    private class Baker:Baker<RotateCubeAuthoring> {
        public override void Bake(RotateCubeAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateCube());
        }
    }
}


public struct RotateCube : IComponentData {

}