using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    //플레이어 컴포넌트 저장
    public PlayerStats stats;
    public PlayerController controller;
    public Animation animation;

    //플레이어 상태에 대한 조절

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<PlayerController>();
        animation = GetComponent<Animation>();
    }
    
}
