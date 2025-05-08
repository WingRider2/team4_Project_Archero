using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] MonsterBase monPre;
    public bool MakeMon(Vector2 pos)
    {
        MonsterBase mon = Instantiate(monPre, pos, Quaternion.identity);
        mon.Init(0, "테스트", 10, 10, 10, 5, 2);
        if(mon==null)
        {
            Debug.Log("몬스터 생성 실패");
            return false;
        }
        mon.SetTarget(GameObject.FindWithTag("Player").transform);
        return true;
        
    }
    public void Start()
    {
        MakeMon(Vector2.zero);

    }
}
