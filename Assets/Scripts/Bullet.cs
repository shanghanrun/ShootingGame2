using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3;
    void Update()
    {
        Vector3 dir = Vector3.up;

        transform.position += dir *speed *Time.deltaTime;
    }

    // void OnCollisionEnter(Collision other){
    //     Destroy(other.gameObject);
    //     Destroy(gameObject);
    // }
}
