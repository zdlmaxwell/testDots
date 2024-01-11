using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootText : MonoBehaviour
{
    private float destoryTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 2f;
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        destoryTimer -= Time.deltaTime;
        if (destoryTimer < 0f) {
            Destroy(gameObject);
        }


    }
}
