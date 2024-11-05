using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp = 20;
    public float speed = 0.02f;
    Vector3 dir;
    float fireInterval = 2f;
    float fireTimer; // 발사간격을 위한 타이머 변수
    public GameObject ballPrefab; // 파이어볼
    public Transform[] firePositions;

    public GameObject explosionPrefab;
    void Start()
    {
        dir = Vector3.down;
        fireTimer = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // StartCoroutine(Fire());
        transform.position += dir * speed *Time.deltaTime;

        // 파이어볼 발사
        fireTimer -= Time.deltaTime;
        if(fireTimer <=0){
            // Fire();
            fireTimer = fireInterval; //초기화
        }
    }

    void Fire(){
        // yield return new WaitForSeconds(fireInterval);

        for(int i=0; i<firePositions.Length; i++){
            GameObject fireBall = Instantiate(ballPrefab);
            fireBall.transform.position = firePositions[i].transform.position;

        }

    }

    void OnCollisionEnter(Collision other)
    {
        // 에너미는 플레이어, 총알과 상관없이 충돌하면 폭발효과 낼 거다.
        // 그래서 CompareTag 사용안함
        GameObject explosion = Instantiate(explosionPrefab);
        //폭발 위치
        explosion.transform.position = transform.position;

        // if(other.gameObject.name.Contains("Bullet)){
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
        }
        else
        { // Bullet  이외의 것은 
            Destroy(other.gameObject);
        }

        hp--;
        if(hp <=0){
            Destroy(gameObject);
        }

        // 에너미 잡을 때마다 현재 점수를 업데이트
        // GameObject smObject = GameObject.Find("ScoreManager");
        // ScoreManager sm = smObject.GetComponent<ScoreManager>(); //스크립트 인스턴스
        ScoreManager.Instance.Score++;
    }
}
