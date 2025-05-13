using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MWeaponHandler : MonoBehaviour
{
    public float speed;
    public float duration;
    public MprohectileController controller;

    public void Attack(Vector2 dir)
    {
        GameObject go = ObjectPoolManager.Instance.GetObject(PoolType.MonsterBullet);
        if (go.TryGetComponent<MprohectileController>(out var bullet))
        {
            go.transform.position = this.transform.position;
            bullet.Init(dir, this);
        }
    }
}