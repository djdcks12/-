using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class BarAgent : Agent
{
    private Rigidbody2D barrigidbody;

    GameObject[] Target_Bricks;
    GameObject Circle;
    public float moveForce = 2000f;
    public Ballscript ballscript;
  
   

    void Awake()
    {
        Target_Bricks = new GameObject[37];
        barrigidbody = GetComponent<Rigidbody2D>();

        Circle = transform.parent.GetChild(1).gameObject;
        for (int i = 0; i < 37; i++)
            Target_Bricks[i] = transform.parent.GetChild(2+i).gameObject;
    }

    void ResetTarget()
    {
        for (int i = 0; i < 37; i++)
            Target_Bricks[i].SetActive(true);
        ballscript.targetEaten = false;
    }

    public override void AgentReset() //Reset함수
    {
        Vector3 random_ball_vel = new Vector3(-100f, -100f, 0); //공의 속력
        Vector3 random_bar_pos = new Vector3(0, -480f, 0);    //막대의 위치
        Vector3 random_ball_pos = new Vector3(0, -100f,0);     //공의 위치

        transform.localPosition = random_bar_pos;
        barrigidbody.velocity = Vector2.zero;

        Circle.transform.localPosition = random_ball_pos;
        Circle.GetComponent<Rigidbody2D>().velocity = random_ball_vel;

        ResetTarget(); //벽돌을 Reset
    }

    public override void CollectObservations() //정보를 수집하는 함수
    {
        
        Vector3 firstBlock = new Vector3(-579f, 283f, 0f); // 안쪽 첫번째 벽돌
        Vector3 secondBlock = new Vector3(-77f, 283f, 0f);// 안쪽 두번째 벽돌
        Vector3 thirdBlock = new Vector3(682f, 283f, 0f); // 안쪽 세번째 벽돌

       
        Vector3 circlePos = Circle.transform.localPosition; //공의 위치

        Vector3 first_distance = firstBlock - circlePos;  //공과 첫번째 벽돌의 거리
        Vector3 second_distance = secondBlock - circlePos;//공과 두번째 벽돌의 거리
        Vector3 third_distance = thirdBlock - circlePos;//공과 세번째 벽돌의 거리

        AddVectorObs(Mathf.Clamp(first_distance.x / 1500f, -1f, 1f));  //공과 첫번째 벽돌의 거리(x축)
        AddVectorObs(Mathf.Clamp(first_distance.y / 736f, -1f, 1f));//공과 첫번째 벽돌의 거리(y축)

        AddVectorObs(Mathf.Clamp(second_distance.x / 920f, -1f, 1f)); //공과 두번째 벽돌의 거리(x축)
        AddVectorObs(Mathf.Clamp(second_distance.y / 736f, -1f, 1f));//공과 두번째 벽돌의 거리(y축)

        AddVectorObs(Mathf.Clamp(third_distance.x / 1500f, -1f, 1f));//공과 세번째 벽돌의 거리(x축)
        AddVectorObs(Mathf.Clamp(third_distance.y / 736f, -1f, 1f));//공과 세번째 벽돌의 거리(y축)

        AddVectorObs(Mathf.Clamp(circlePos.x / 850f, -1f, 1f)); //공의 위치(x축)
        AddVectorObs(Mathf.Clamp(circlePos.y / 500f, -1f, 1f)); //공의 위치(y축)

        bool firstblock_status = Target_Bricks[32].activeInHierarchy; //첫번째 벽돌의 상태
        bool secondblock_status = Target_Bricks[13].activeInHierarchy;//두번째 벽돌의 상태
        bool thirdblock_status = Target_Bricks[28].activeInHierarchy;//세번째 벽돌의 상태

        AddVectorObs(firstblock_status);//첫번째 벽돌의 상태 수집
        AddVectorObs(secondblock_status);//두번째 벽돌의 상태 수집
        AddVectorObs(thirdblock_status);//세번째 벽돌의 상태 수집
    }

    public override void AgentAction(float[] vectorAction, string textAction) //Agent의 액션을 제어하는 함수
    { 
        AddReward(-0.001f);//정지하는 상태를 대비하여 정지상태에 있을 경우 보상 감소
        float horizontalInput = vectorAction[0];
        
        barrigidbody.AddForce(new Vector2(horizontalInput * moveForce, 0f));

        if (ballscript.targetEaten) //공이 벽돌을 깨는 경우
        {
            AddReward(1f); //보상 +1
            ballscript.targetEaten = false; //벽돌의 상태 false 전환
        }
        else if (ballscript.dead) // 공이 바닥에 떨어지는 경우
        {
            AddReward(-1f); //보상 -1

            Done(); //종료 ---->Reset
            ballscript.dead = false;
        }
        Monitor.Log(name, GetCumulativeReward(), transform);
    }
}
