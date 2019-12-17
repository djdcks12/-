using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool dead;
    public GameObject target;

    public float speed = 8f; // 총알 이동 속도
    // 이동에 사용할 리지드바디 컴포넌트
    private Rigidbody bulletRigidbody;
    static int playerindex;
    void Start()
    {
        // 자신의 게임 오브젝트에서 Rigidbody를 찾아가져오기
        bulletRigidbody = GetComponent<Rigidbody>();

        // 리지드바디의 속도 = 앞쪽 방향 * 속력
        // transform은 자신의 트랜스폼 컴포넌트를 즉시
        // 접근하는 지름길
        bulletRigidbody.velocity
            = transform.forward * speed;


        // 자신의 게임 오브젝트를 3초 뒤에 파괴
        //Destroy(gameObject, 3f);
    }

    // 트리거 충돌 : 서로 뚫고 지나가는 충돌
    // 트리거 충돌시 자동실행 (입력으로 상대방 콜라이더가 옴)
    public void OnTriggerEnter(Collider other)
    {
        
        // 충돌한 상대방의 태그가 Player 인가?
        if (other.gameObject == target)
        {
            // 그러하다면, 상대방으로부터
            // PlayerController 컴포넌트를 가져오기
            dead = true;
            Destroy(gameObject);

            /*foreach(GameObject bul in GameObject.FindGameObjectsWithTag("dead"))
            {
                
                Destroy(bul);
            }*/
            char[] c = other.gameObject.tag.ToCharArray();
            playerindex = int.Parse(c[6].ToString()) - 1;
            switch (playerindex)
            {
                case 0:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead")) Destroy(bul);

                    break;
                case 1:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead2")) Destroy(bul);
                    break;
                case 2:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead3")) Destroy(bul);
                    break;
                case 3:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead4")) Destroy(bul);
                    break;
                case 4:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead5")) Destroy(bul);
                    break;
                case 5:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead6")) Destroy(bul);
                    break;
                case 6:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead7")) Destroy(bul);
                    break;
                case 7:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead8")) Destroy(bul);
                    break;
                case 8:
                    foreach (GameObject bul in GameObject.FindGameObjectsWithTag("dead9")) Destroy(bul);
                    break;
            }

            BulletSpawner.Instance.bullets.Clear();
        }

    }
    private void FixedUpdate()
    {/*
        float x = 151;
        float y = -91.3f;
        char[] c = target.gameObject.tag.ToCharArray();
        playerindex = int.Parse(c[6].ToString()) - 1;
        switch (playerindex)
         {
             case 0:
                 if (transform.localPosition.x > 74.5f || transform.localPosition.y > 36.8 || transform.localPosition.x < -72.8 || transform.localPosition.y < -48)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }

                 break;
             case 1:
                 if (transform.localPosition.x > 74.5f+x || transform.localPosition.y > 36.8 || transform.localPosition.x < -72.8+x || transform.localPosition.y < -48)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }
                 break;
             case 2:
                 if (transform.localPosition.x > 74.5f + x*2 || transform.localPosition.y > 36.8 || transform.localPosition.x < -72.8 + x*2 || transform.localPosition.y < -48)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }
                 break;
             case 3:
                 if (transform.localPosition.x > 74.5f || transform.localPosition.y > 36.8+y || transform.localPosition.x < -72.8 || transform.localPosition.y < -48+y)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }

                 break;
             case 4:
                 if (transform.localPosition.x > 74.5f + x || transform.localPosition.y > 36.8+y || transform.localPosition.x < -72.8 + x || transform.localPosition.y < -48+y)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }
                 break;
             case 5:
                 if (transform.localPosition.x > 74.5f + x * 2 || transform.localPosition.y > 36.8+y || transform.localPosition.x < -72.8 + x * 2 || transform.localPosition.y < -48+y)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }
                 break;
             case 6:
                 if (transform.localPosition.x > 74.5f || transform.localPosition.y > 36.8 + y*2 || transform.localPosition.x < -72.8 || transform.localPosition.y < -48 + y*2)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }

                 break;
             case 7:
                 if (transform.localPosition.x > 74.5f + x || transform.localPosition.y > 36.8 + y*2 || transform.localPosition.x < -72.8 + x || transform.localPosition.y < -48 + y*2)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }
                 break;
             case 8:
                 if (transform.localPosition.x > 74.5f + x * 2 || transform.localPosition.y > 36.8 + y*2 || transform.localPosition.x < -72.8 + x * 2 || transform.localPosition.y < -48 + y*2)
                 {
                     Destroy(gameObject);
                     BulletSpawner.Instance.bullets.Remove(gameObject);
                 }
                 break;
         }
         */
        
    }
}
    
