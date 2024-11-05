using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float limitX_min = -2.9f;
    [SerializeField] float limitX_max = 2.9f;
    [SerializeField] float limitY_min = -4.5f;
    [SerializeField] float limitY_max = 4.5f;
    public float speed = 8;
    public float evasiveDistance = 4f; // z axis
    public float evasiveDuration = 1f;
    public float horizontalDistance = 2f; 
    int rotationDir = 1;
    bool isEvasiveManuever = false;

    Collider coll;
    Rigidbody rb;
    AudioSource audio1;

    void Start(){
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        audio1 = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 p = transform.position;
        if( p.x >= limitX_min && p.x <= limitX_max && p.y >= limitY_min && p.y <= limitY_max){
            if (! isEvasiveManuever){
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                Vector3 dir = new Vector3(h,v,0);

                transform.position += dir *speed *Time.deltaTime; 
            }
            if(Input.GetKeyDown(KeyCode.Z) && !isEvasiveManuever){
                StartCoroutine(EvasiveManuever());
            }
        }
        // 제한 범위를 벗어났을 경우, 다시 제한범위 안으로 넣어주어야
        // 방향키를 통해 조정이 가능해진다.
        else
        {
            // 제한 값으로 위치를 고정
            float clampedX = Mathf.Clamp(p.x, limitX_min, limitX_max);
            float clampedY = Mathf.Clamp(p.y, limitY_min, limitY_max);

            transform.position = new Vector3(clampedX, clampedY, p.z);
        }
    }
    

    IEnumerator EvasiveManuever(){
        isEvasiveManuever = true;
        if(audio1 != null) audio1.Play();
        if (transform.position.x >0){
            horizontalDistance = -Mathf.Abs(horizontalDistance);
            rotationDir = Mathf.Abs(rotationDir);
        } else{
            horizontalDistance = Mathf.Abs(horizontalDistance);
            rotationDir = -Mathf.Abs(rotationDir);
        }

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 startScale = transform.localScale;

        Vector3 targetPosition = startPosition + new Vector3(horizontalDistance,0, -evasiveDistance);

        // 회전하는 동안 충돌방지 위해
        if(coll !=null){coll.enabled = false;}
        if(rb !=null){rb.isKinematic = true;}

        float elapsedTime =0f;

        while(elapsedTime < evasiveDuration){
            float t = elapsedTime / evasiveDuration;

            // 원형 경로 및 x축을 따라 회전 플립적용
            float angle = 360 *t * rotationDir; // 360도 회전을 시간에 따라 나누어 작동

            transform.position = Vector3.Lerp(startPosition,targetPosition, t) + new Vector3(0,0, -Mathf.Sin(Mathf.PI *t)* evasiveDistance);

            // 회전을 적용하여 플립 모션을 만듬
            // transform.rotation = startRotation * Quaternion.Euler(angle,0,0);
            // 회전을 적용하여 플립 모션을 만듬(y축)
            transform.rotation = startRotation * Quaternion.Euler(0,angle,0);

            // 크기조정( 2.5배가 되었다가 다시 돌아옴)
            float scaleMultiplier = Mathf.Lerp(1f,2.5f,Mathf.Sin(Mathf.PI *t));
            transform.localScale = startScale * scaleMultiplier;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 처음 상태 + 알파 위치로 돌아가기
        transform.position = startPosition + new Vector3(horizontalDistance,0,0);
        transform.rotation = startRotation;
        transform.localScale = startScale;

        // coll, rb 원상복귀
        if(coll !=null){coll.enabled = true;}
        if(rb !=null){rb.isKinematic = false;}

        isEvasiveManuever = false;
    }
}
