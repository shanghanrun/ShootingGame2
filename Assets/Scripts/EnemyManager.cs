using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    public int poolSize =10;
    // GameObject[] enemyObjectPool;
    public static List<GameObject> enemyObjectPool;
    public Transform[] spawnPoints;

    float currentTime;
    public float createTime =2;

    //생성 최소시간, 최대시간
    float minTime = 0.2f;
    float maxTime = 1f;

    void Start(){
        //태어날 때 적의 생성시간을 설정
        createTime = Random.Range(minTime, maxTime);

        // 오브젝트풀 만들기
        // enemyObjectPool = new GameObject[poolSize];
        enemyObjectPool = new List<GameObject>();

        for (int i=0; i<poolSize; i++){
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyObjectPool.Add(enemy);
        }
    }
    
    void Update(){
        currentTime += Time.deltaTime;

        if(currentTime > createTime){
            if(enemyObjectPool.Count >0){
                GameObject enemy = enemyObjectPool[0];
                enemy.SetActive(true);
                enemyObjectPool.Remove(enemy);

                int index = Random.Range(0, spawnPoints.Length);
                enemy.transform.position = spawnPoints[index].position;
            }
            
            // for(int i=0; i<poolSize; i++){
            //     GameObject enemy = enemyObjectPool[i];
            //     int index = Random.Range(0, spawnPoints.Length); 

            //     if(enemy.activeSelf == false){
            //         // enemy.transform.position = transform.position;
            //         enemy.transform.position = spawnPoints[index].position;

            //         enemy.SetActive(true);

            //         //단 하나 생성
            //         break;
            //     }
            // }

            // 적을 생상한 후, 적의 생성시간을 다시 설정(왜냐면 Start는 한번만 실행. 그래서 고정된다.)

            currentTime =0;
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
