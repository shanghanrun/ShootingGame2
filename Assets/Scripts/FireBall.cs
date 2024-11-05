using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    GameObject player;
    Vector3 dir;
    public float speed = 1.5f;
    
    void Start()
    {
        // player = GameObject.Find("Player");
        dir = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        // dir = (player.transform.position - transform.position) * -1;
        // dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;
    }
}
