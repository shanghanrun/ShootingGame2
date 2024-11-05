using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5;
    GameObject player; //  나중에 충돌대비 
    [SerializeField] GameObject explosionPrefab;

    Vector3 dir;

    void Start()
    {
        //0부터 9까지  10개의 값 중에서 하나 랜덤
        int randValue = Random.Range(0, 10);
        // 30 % = 3개만 고름
        if (randValue < 3)
        {
            // 플레이어를 타겟으로 삼는다.
            GameObject target = GameObject.Find("Player");
            // 방향구하기  target -me
            if(target !=null){
                // 플레이어가 죽으면 에러난다. 그래서 null 체크
                dir = target.transform.position - transform.position;
                dir.Normalize(); //정규화
            }
        }
        else
        {
            dir = Vector3.down;
        }
    }
    void OnEnable(){
        //0부터 9까지  10개의 값 중에서 하나 랜덤
        int randValue = Random.Range(0, 10);
        // 30 % = 3개만 고름
        if (randValue < 3)
        {
            // 플레이어를 타겟으로 삼는다.
            GameObject target = GameObject.Find("Player");
            // 방향구하기  target -me
            if (target != null)
            {
                // 플레이어가 죽으면 에러난다. 그래서 null 체크
                dir = target.transform.position - transform.position;
                dir.Normalize(); //정규화
            }
        }
        else
        {
            dir = Vector3.down;
        }
    }
    void Update()
    {
        // Vector3 dir = Vector3.down; 삭제
        transform.position += dir *speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other){
        // 에너미는 플레이어, 총알과 상관없이 충돌하면 폭발효과 낼 거다.
        // 그래서 CompareTag 사용안함
        GameObject explosion = Instantiate(explosionPrefab);
        //폭발 위치
        explosion.transform.position = transform.position;

        // if(other.gameObject.name.Contains("Bullet)){
        if(other.gameObject.CompareTag("Bullet")){
            other.gameObject.SetActive(false);
            //리스트에 넣을 경우에는 PlayerFire 클래스를 동원해야 리스트를 얻을 수 있다.
            // GameObject player = GameObject.Find("Player");
            // PlayerFire playerFire = player.GetComponent<PlayerFire>();
            PlayerFire.bulletObjectPool.Add(other.gameObject);
        }
        else{ // Bullet  이외의 것은 
            Destroy(other.gameObject);
        }
        
        // Destroy(gameObject);
        gameObject.SetActive(false);

        // GameObject enemyMg = GameObject.Find("EnemyManager");
        // EnemyManager enemyManager = enemyMg.GetComponent<EnemyManager>();
        EnemyManager.enemyObjectPool.Add(gameObject);


        // 에너미 잡을 때마다 현재 점수를 업데이트
        // GameObject smObject = GameObject.Find("ScoreManager");
        // ScoreManager sm = smObject.GetComponent<ScoreManager>(); //스크립트 인스턴스
        ScoreManager.Instance.Score++;
    }
}
