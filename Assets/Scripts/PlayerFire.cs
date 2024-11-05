using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //총알 생산공장을 외부에서 입력받음
    [Header("총알 프리팹")]
    [SerializeField] GameObject bulletPrefab;
    public int poolSize = 10;
    GameObject[] bulletObjectPool;

    [Header("총구 위치")]
    //총알발사위치(총구)
    [SerializeField] GameObject FirePosition;

    void Start(){
        bulletObjectPool = new GameObject[poolSize];

        for(int i =0; i<poolSize; i++){
            GameObject bullet = Instantiate(bulletPrefab);
            
            // 풀에 넣는다.
            bulletObjectPool[i] = bullet;
            // 비활성화
            bullet.SetActive(false);
        }
    }

    void Update(){
        if (Input.GetButtonDown("Fire1")){

            //! 이미 활성화된 것은 빼고, 비활성화된 총알만 활성화시킨다.
            for(int i=0; i<poolSize; i++){
                GameObject bullet = bulletObjectPool[i];
                if(bullet?.activeSelf == false){
                    bullet.SetActive(true);
                    bullet.transform.position = transform.position;

                    // 하나만 발사되고, 더 발사되지 않도록
                    break; // for문 빠져나간다.
                }
            }
            
        }
    }
}
