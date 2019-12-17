using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaking : MonoBehaviour
{
    public TrapReward Maps; // 복사해서 가져올 원본 맵
    public TrapReward[] Map_Ins = new TrapReward[19];  // 생성될 맵들
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 19; i++)
            Map_Ins[i] = Instantiate(Maps, new Vector3((float)(95.5 - 5 * i), 0, transform.parent.position.z), Quaternion.identity, transform.parent);
        // 적절한 위치 간격으로 복사한 맵을 생성함
        Maps.gameObject.SetActive(false);  // 복사해 온 원본 맵은 꺼준다.
    }
    public void Make_Map()  // 맵 만들어주는 함수
    {
        foreach (var item in Map_Ins)
        {
            item.Rocating();  // 각각의 맵에서 로케이팅을 시킴
        }
    }

}