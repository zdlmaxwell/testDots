using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StunnedAuthoring : MonoBehaviour{
    public class Baker : Baker<StunnedAuthoring> {
        public override void Bake(StunnedAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new stunned());
            SetComponentEnabled<stunned>(entity, false);
        }
    }
}


public struct stunned : IComponentData, IEnableableComponent {

}