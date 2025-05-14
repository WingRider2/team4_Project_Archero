using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterBoss : MonsterBase
{

    MWeaponHandler m_Controller;
    [SerializeField] AudioClip audio;
    protected override void Awake()
    {
        base.Awake();
        SoundManager.PlayClip(audio);
        m_Controller = GetComponentInChildren<MWeaponHandler>();

    }
    public  void Attack(int a)
    {

        Attack();
       
            Vector2 dir = Target.position - transform.position;
        if (a == 1)
        {
            soundPlayer.Play(UnitSoundType.Skill, 0);
            m_Controller.CircleAttack();
        }
        if (a == 2)
        {
            m_Controller.Attack(dir);
            soundPlayer.Play(UnitSoundType.Skill, 1);
        }
        if (a == 3)
        {
            m_Controller.ZoneAttack(Target.position);
            soundPlayer.Play(UnitSoundType.Skill, 2);
        }

        
    }
    IEnumerator Timer(float time, System.Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
    public bool Teleport(Vector2 pos)
    {
        Debug.Log("이동");
        transform.position = pos+new Vector2(0,1);
        return true;
    }
}
