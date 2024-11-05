using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            //리스트에 넣을 경우에는 PlayerFire 클래스를 동원해야 리스트를 얻을 수 있다.
            // GameObject player = GameObject.Find("Player");
            // PlayerFire playerFire = player.GetComponent<PlayerFire>();
            PlayerFire.bulletObjectPool.Add(other.gameObject);

        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            // GameObject enemyMg = GameObject.Find("EnemyManager");
            // EnemyManager enemyManager = enemyMg.GetComponent<EnemyManager>();
            EnemyManager.enemyObjectPool.Add(other.gameObject);
        }
        else{
            Destroy(other.gameObject);
        }
    }
}
