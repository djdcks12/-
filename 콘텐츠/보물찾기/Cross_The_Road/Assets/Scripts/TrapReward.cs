using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapReward : MonoBehaviour
{
    public GameObject[] Obj;  // obj 0~2 까지 각각 왼쪽,중앙,오른쪽 보물 / 3~5 까지 각각 왼쪽,중앙,오른쪽 몬스터

    public int ran;
    void Start()
    {
        Rocating();          // 시작될 때 배치 시작
    }

    public void Rocating()
    {
        foreach (var item in Obj)
        {
            item.SetActive(false); //모든 요소 꺼주고
        }

        ran = Random.Range(0, 3); //랜덤 돌려서

        //각각 한쪽만 보물 켜주고 나머지에 몬스터 배치
        if (ran == 0) { Obj[0].SetActive(true); Obj[4].SetActive(true); Obj[5].SetActive(true); }   
        else if (ran == 1) { Obj[1].SetActive(true); Obj[3].SetActive(true); Obj[5].SetActive(true); }
        else { Obj[2].SetActive(true); Obj[3].SetActive(true); Obj[4].SetActive(true); }
        
    }

}
