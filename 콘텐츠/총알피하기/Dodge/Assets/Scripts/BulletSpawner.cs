using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject[] wall;
        public GameObject bulletPrefab; // 생성할 총알 원본 프리팹
        public float spawnRateMin = 0.5f; // 최소 생성 주기
        public float spawnRateMax = 3f; // 최대 생성 주기
    public bool isGameover;
        private Transform target; // 발사할 대상
    public GameObject player;
        private float spawnRate; // 생성 주기
        private float timeAfterSpawn; // 최근 생성 시점에서 지난 시간
    public  List<GameObject> bullets;
    public int playerindex;
    public List<GameObject> bullets1;
    public List<GameObject> bullets2;
    public List<GameObject> bullets3;
    public List<GameObject> bullets4;
    public List<GameObject> bullets5;
    public List<GameObject> bullets6;
    public List<GameObject> bullets7;
    public List<GameObject> bullets8;
    bool isInit;

    private static BulletSpawner _bullet;
    public static BulletSpawner Instance { get { return _bullet; } }

    private void Awake()
    {
        _bullet = GetComponent<BulletSpawner>();

    }

    void Start()
        {
            timeAfterSpawn = 0f; // 타이머를 리셋
                                 // spawnRateMin과 spawnRateMax 사이의 랜던 값을 사용
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            // FindObjectOfType은 씬에 존재하는 모든 오브젝트를 검색
            // 하여 원하는 타입의 오브젝트를 찾아옴
            target = player.transform;
        }
    void Init()
    {
        foreach (GameObject bul in bullets) Destroy(bul);
        isInit = false;
        int pos = Random.Range(1, 4);
        GameObject bullet;
        switch (pos)
        {
            case 1:
                 bullet = Instantiate(bulletPrefab,
                new Vector3(Random.Range(wall[2].transform.position.x - 2f, wall[3].transform.position.x + 2f), Random.Range(wall[0].transform.position.y, wall[0].transform.position.y+ 2f)), transform.rotation);
                break;
            case 2:
                bullet = Instantiate(bulletPrefab,
                new Vector3(Random.Range(wall[2].transform.position.x - 2f, wall[3].transform.position.x + 2f), Random.Range(wall[1].transform.position.y - 2f, wall[1].transform.position.y)), transform.rotation);
                break;            
            case 3:
                bullet = Instantiate(bulletPrefab,
                new Vector3(Random.Range(wall[2].transform.position.x-2f, wall[2].transform.position.x), Random.Range(wall[1].transform.position.y - 2f, wall[0].transform.position.y+2f)), transform.rotation);
                break;
            case 4:
                bullet = Instantiate(bulletPrefab,
                new Vector3(Random.Range(wall[3].transform.position.x , wall[3].transform.position.x + 2f), Random.Range(wall[1].transform.position.y - 2f, wall[0].transform.position.y+2f)), transform.rotation);
                break;
            default:
                return;

        }
        isGameover = true;
    }
    public void RemoveFromList(GameObject gm)
    {
        bullets.Remove(gm);
    }
    void Update()
    {

        if (!isGameover)
        {
            // Time.deltaTime은 직전의 Update와 현재 Update 실행 시점
            // 사이의 시간 간격
            timeAfterSpawn = timeAfterSpawn + Time.deltaTime;
            // 누적된 시간이 생성 주기보다 크거나 같다
            if (timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0f; // 누적된 시간을 리셋

                // bulletPrefab의 복제본을 생성
                // 위치와 회전은 총알 생성기 자신의 위치와 회전으로 지정.
                // 생성된 총알 복제본을 bullet 이라는 변수로 다루기
                int pos = Random.Range(1, 4);
                GameObject bullet;
                switch (pos)
                {
                    case 1:
                        bullet = Instantiate(bulletPrefab,
                       new Vector3(Random.Range(wall[2].transform.position.x - 2f, wall[3].transform.position.x + 2f), Random.Range(wall[0].transform.position.y, wall[0].transform.position.y + 2f)), transform.rotation);
                        break;
                    case 2:
                        bullet = Instantiate(bulletPrefab,
                        new Vector3(Random.Range(wall[2].transform.position.x - 2f, wall[3].transform.position.x + 2f), Random.Range(wall[1].transform.position.y - 2f, wall[1].transform.position.y)), transform.rotation);
                        break;
                    case 3:
                        bullet = Instantiate(bulletPrefab,
                        new Vector3(Random.Range(wall[2].transform.position.x - 2f, wall[2].transform.position.x), Random.Range(wall[1].transform.position.y - 2f, wall[0].transform.position.y + 2f)), transform.rotation);
                        break;
                    case 4:
                        bullet = Instantiate(bulletPrefab,
                        new Vector3(Random.Range(wall[3].transform.position.x, wall[3].transform.position.x + 2f), Random.Range(wall[1].transform.position.y - 2f, wall[0].transform.position.y + 2f)), transform.rotation);
                        break;
                    default:
                        return;

                }
                
                // 총알이 target을 바라보도록 회전
                char[] c = player.gameObject.tag.ToCharArray();
                playerindex = int.Parse(c[6].ToString())-1;
                switch (playerindex)
                {
                    case 0:
                        bullet.tag = "dead";
                        break;
                    case 1:
                        bullet.tag = "dead2";
                        break;
                    case 2:
                        bullet.tag = "dead3";
                        break;
                    case 3:
                        bullet.tag = "dead4";
                        break;
                    case 4:
                        bullet.tag = "dead5";
                        break;
                    case 5:
                        bullet.tag = "dead6";
                        break;
                    case 6:
                        bullet.tag = "dead7";
                        break;
                    case 7:
                        bullet.tag = "dead8";
                        break;
                    case 8:
                        bullet.tag = "dead9";
                        break;
                }
                bullet.transform.LookAt(target);
                
                // 다음번 생성까지의 생성 간격을 랜덤하게 변경
                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            }
        }
        else
        {
            if (isInit) { Init(); }
        }
    }
    }
    
