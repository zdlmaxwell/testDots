using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
   public GameObject ShootPrefab;
    private void Start() {
        PlayerShootingSystem playerShootingSystem =
            World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
        playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    }

    void PlayerShootingSystem_OnShoot(object sender, System.EventArgs e) {
        Entity playerEntity = (Entity)sender;
        LocalTransform localTransform = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
        Instantiate(ShootPrefab, localTransform.Position,Quaternion.identity);
    }
}
