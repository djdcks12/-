using MLAgents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : Agent
{
    private Rigidbody playerrigidbody;
public Transform target;
    public GameObject bomb;
    Transform target2;
    public float moveForce = 10f;
    public BulletScript bulletScript;
    static int playerindex;
    public bool dead = false;
    public float horizontalInput;
    public float verticalInput;
    public bool dead2 = false;
    public GameObject[] panel1;
    public JudgePanel judge;
    public Vector3 movezonesize;
    public Vector3 movezonepos;
    private static PlayerAgent _player;
    public bool[] isjudge;
    static Vector3[] trans;
    public static PlayerAgent Instance { get { return _player; } }
    void Awake()
    {
        trans = new Vector3[8];
        
        playerrigidbody = GetComponent<Rigidbody>();
        for (int i = 0; i < 8; i++)
            trans[i] = panel1[i].gameObject.transform.localPosition;
        var movezone = GameObject.Find("MoveZone");
        movezonepos = movezone.transform.position;
        judge = GameObject.Find("judge0").GetComponent<JudgePanel>();
        movezonesize.x = movezone.GetComponent<Renderer>().bounds.size.x;
        movezonesize.y = movezone.GetComponent<Renderer>().bounds.size.y;
        isjudge = new bool[8];
        movezonepos.x -= movezonesize.x / 2;
        char[] c = gameObject.tag.ToCharArray();
        playerindex = int.Parse(c[6].ToString()) - 1;
        movezonepos.y -= movezonesize.y / 2;
        
    }
    private void OnTriggerEnter(Collider other) //기체에 총알이 부딪혔을 시
    {
        string deadindex = "dead"; //총알에 맞은 여부 true
        char[] c = gameObject.tag.ToCharArray(); //태그를 통해 설정된 각 플레이보드 상의 플레이어 구별을 위한 처리
        playerindex = int.Parse(c[6].ToString()) - 1; 
        switch (playerindex) //플레이보드 상의 총알 구별 및 index값 할당 
        {
            case 0:
                deadindex = "dead";
                break;
            case 1:
                deadindex = "dead2";
                break;
            case 2:
                deadindex = "dead3";
                break;
            case 3:
                deadindex = "dead4";
                break;
            case 4:
                deadindex = "dead5";
                break;
            case 5:
                deadindex = "dead6";
                break;
            case 6:
                deadindex = "dead7";
                break;
            case 7:
                deadindex = "dead8";
                break;
            case 8:
                deadindex = "dead9";
                break;
                
        } 
        if (other.CompareTag(deadindex)) //기체에 닿은 총알이 알맞은 플레이어보드 상의 총알일 때
        {
            bomb.gameObject.SetActive(true); //폭발 이펙트 활성화
            dead = true; //총알에 맞은 여부 true
        }
    }
    public override void AgentReset() //Reset 함수
    {
        transform.localPosition = new Vector3(0, 0, 0); //초기화할 위치 할당
        dead = false; //총알에 맞은 여부 false
        dead2 = false; //벽에 부딪힌 여부 false
        for (int i = 0; i < 8; i++) //판단 패널 위치 초기화
            panel1[i].transform.localPosition = trans[i];
        bomb.gameObject.SetActive(false); //폭발 이펙트 비활성화
        playerrigidbody.velocity = Vector3.zero; //기체 속도값 초기화
    }
    public override void AgentAction(float[] vectorAction, string textAction) //Agent의 액션을 제어
    {
        AddReward(0.01f); //매 프레임당 보상 +0.01
        horizontalInput = vectorAction[0]; //AI의 input 1번째를 x축 이동에 할당
        verticalInput = vectorAction[1]; //AI의 input 2번째를 y축 이동에 할당
        char[] c = gameObject.tag.ToCharArray(); //태그를 통해 설정된 각 플레이보드 상의 플레이어 구별을 위한 처리
        playerindex = int.Parse(c[6].ToString()) - 1;
        transform.localPosition+=new Vector3(horizontalInput * moveForce/5, verticalInput * moveForce/5, 0f); //해당 AI 입력값으로 기체 이동
        if (transform.localPosition.x > 70.5f||transform.localPosition.y>32.8||transform.localPosition.x<-67.8|| transform.localPosition.y<-43) //기체가 벽면에 닿았을 시
        {
            dead2 = true; //벽에 닿아 죽은 경우의 dead2변수 true로 설정
            
            switch (playerindex) //벽에 닿아 죽었을 시 총알 초기화
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

            BulletSpawner.Instance.bullets.Clear(); //총알 Gameobject가 들어있는 리스트 초기화
        }
        if (dead) //총알에 부딪혔을 시
        {
            AddReward(-1f); //보상 -1
            Done(); //AgentReset함수 호출
            
        }
        if (dead2) //벽에 부딪혔을 시
        {
            AddReward(-5f); //보상 -5
            Done(); //AgentReset함수 호출
        }

        Monitor.Log(name, GetCumulativeReward(), transform); //보상 및 위치값, bool값에 대한 관찰 수집
    }

    public override void CollectObservations() //관찰 정보 수집하는 함수
    {
        foreach(bool b in isjudge) AddVectorObs(b); //8개 판단패널의 각 bool 변수 관찰
        Vector3 relativePos = transform.localPosition; //각 플레이어보드 상의 상대적 위치값 할당
        
        //-5 ~ +5
        AddVectorObs(Mathf.Clamp(relativePos.x / 5f, -1f, 1f)); //기체의 상대적 위치(x축)
        AddVectorObs(Mathf.Clamp(relativePos.y / 5f, -1f, 1f)); //기체의 상대적 위치(y축)

        //-10 ~ +10 -> -1 ~ +1 (정규화)
        // if -3 이면 -0.3 으로
        AddVectorObs(Mathf.Clamp(playerrigidbody.velocity.x / 10f, -1f, 1f)); //기체의 속도(x축)
        AddVectorObs(Mathf.Clamp(playerrigidbody.velocity.y / 10f, -1f, 1f)); //기체의 속도(y축)
    }
}

