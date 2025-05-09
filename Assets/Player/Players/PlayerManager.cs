using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    //�÷��̾� ������Ʈ ����
    public PlayerStats stats;
    public PlayerController controller;
    public Animation animation;

    //�÷��̾� ���¿� ���� ����

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<PlayerController>();
        animation = GetComponent<Animation>();
    }
    
}
