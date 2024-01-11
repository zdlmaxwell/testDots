using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour{

    private class Baker : Baker<PlayerAuthoring> {
        public override void Bake(PlayerAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Player {

            });
        }
    }
}

public struct Player : IComponentData {

}