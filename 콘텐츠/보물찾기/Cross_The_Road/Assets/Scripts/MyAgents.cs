using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class MyAgents : Agent
{
    public Player player;

    public override void AgentReset()  // 리셋할때 일어나는 일
    {
        player.MapReMaking();  //맵 재 생성함
        this.gameObject.transform.localPosition = new Vector3(93f,-1.65f,0f); // player 위치 초기화
    }
    public override void AgentAction(float[] vectorAction, string textAction) // 매 순간 발생하는 일
    {
        AddReward(-0.001f);                              // 빨리 가기위해 지속적으로 리워드를 감소 시킴
        float HorizontalInput = vectorAction[0];     // HorizontalInput 변수 생성
        float VerticalInput = vectorAction[1];     // HorizontalInput 변수 생성

        // Input에 따른 좌우 이동
        if (HorizontalInput < 0)        
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x,-1.65f, -2.5f);
        if (HorizontalInput > 0)
            this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, -1.65f, 2.5f);

        // Input에 따른 중앙 및 앞으로 이동
        if (VerticalInput > 0) this.gameObject.transform.localPosition += new Vector3(-0.2f, 0f, 0f);
        else this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, -1.65f, 0);

        // player class의 bool 변수 결과에따라 Reward및 초기화 처리
        if (player.IsEaten) { AddReward(0.5f); player.IsEaten = false; }
        if(player.IsCrash) { AddReward(-1f); player.IsCrash = false; Done(); }
        if (player.isEnd) {  player.isEnd = false; Done(); }

        Monitor.Log(name, GetCumulativeReward(), transform);
    }
    public override void CollectObservations() // 저장할 환경변수 설정
    {
        AddVectorObs(transform.position.z);  // player의 현재 Z축 위치(좌 중 우)

        foreach (var item in player.IsTresure) // 바로 앞에있는 왼쪽 중앙 오른쪽의 보물상자 유무
        {
            AddVectorObs(item);
        }

        foreach (var item in player.IsEnemey) // 바로 앞에있는 왼쪽 중앙 오른쪽의 몬스터 유무
        {
            AddVectorObs(item);
        }
    }
}
