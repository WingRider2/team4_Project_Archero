using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MWeaponHandler : MonoBehaviour
{
    public float speed;
    public float duration;
    public GameObject Circle;
    
    public void Attack(Vector2 dir)
    {
        GameObject go = ObjectPoolManager.Instance.GetObject(PoolType.MonsterBullet);
        if (go == null)
            Debug.Log("총알 없음");
        if (go.TryGetComponent<MprohectileController>(out var bullet))
        {
            go.transform.position = this.transform.position;
            bullet.Init(dir, this);
        }
    }
    public void ZoneAttack(Vector2 pos)
    {
        GameObject go = ObjectPoolManager.Instance.GetObject(PoolType.MonsterZoneBullet);
        if (go == null)
            Debug.Log("총알 없음");
        if (go.TryGetComponent<SkillZone>(out var bullet))
        {
            go.transform.position = this.transform.position;
            bullet.Init(pos, this);
        }
    }
    private int circleCount;

    public void CircleAttack()
    {
        if (circleCount>0)
            return;
       
        int bulletCount = 6;
        circleCount = bulletCount;
        float angleStep=360f/bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            float rad = angle* Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            GameObject go = ObjectPoolManager.Instance.GetObject(PoolType.MonsterCircleBullet);
            if (go == null)
                Debug.Log("총알 없음");
            if (go.TryGetComponent<MCircleProhectile>(out var bullet))
            {
                go.transform.position = this.transform.position + (Vector3)(dir*0.5f);
                bullet.Init(dir, this,angle);
                bullet.onFinished = () => circleCount--;
            }
        }
    }
}