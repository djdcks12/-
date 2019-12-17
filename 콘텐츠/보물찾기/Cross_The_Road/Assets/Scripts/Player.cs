using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MapMaking mapMaking;
    public bool IsEaten;  // 보물상자를 먹었는지
    public bool IsCrash;  // 적과 부딧쳐는지
    public bool isEnd;    // 한 스테이지가 끝났는지

    public bool[] IsTresure = new bool[3]; // 왼쪽,오른쪽,중앙에 보물이 있는지
    public bool[] IsEnemey = new bool[3];  // 왼쪽,오른쪽,중앙에 몬스터가 있는지

    public void MapReMaking() {            // 한 스테이지가 끝나고 맵을 재생성 할 때
        mapMaking.Make_Map();
    }

    void Update()                           // 매 프레임마다 현재 위치에 따른 몬스터 및 보물 존재 유무 업데이트
    {
        if(this.transform.position.x < 93)
            Rebooling(18 - (int)(this.transform.position.x - 4) / 5);
        else Rebooling(0);

    }
    void Rebooling(int i) {              // 몬스터 및 보물 존재 유무를 결정시켜주는 함수
        IsTresure[0] = mapMaking.Map_Ins[i].Obj[0].activeInHierarchy;
        IsTresure[1] = mapMaking.Map_Ins[i].Obj[1].activeInHierarchy;
        IsTresure[2] = mapMaking.Map_Ins[i].Obj[2].activeInHierarchy;
        IsEnemey[0] = mapMaking.Map_Ins[i].Obj[3].activeInHierarchy;
        IsEnemey[1] = mapMaking.Map_Ins[i].Obj[4].activeInHierarchy;
        IsEnemey[2] = mapMaking.Map_Ins[i].Obj[5].activeInHierarchy;

        this.gameObject.transform.localPosition -= new Vector3(0.2f, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)  // 누군가랑 부딧쳤을때 호출
    {
        //각각 상황에따라 bool 변수 true로 올려주고 해당 오브젝트 꺼줌
        if (other.tag.Equals("Reward")) { IsEaten=true; other.gameObject.SetActive(false); }  
        else if (other.tag.Equals("Penalty")) { IsCrash=true; other.gameObject.SetActive(false); }
        else if (other.tag.Equals("End")) isEnd = true;
    }

}
