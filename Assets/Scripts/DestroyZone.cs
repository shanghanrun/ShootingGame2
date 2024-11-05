using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Bullet") ||
            other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
        else{
            Destroy(other.gameObject);
        }
    }
}
