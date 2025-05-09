using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponHandler : MonoBehaviour
{
    // 무기에 대해서
    // 오브젝트 풀은 후에 매니저를 통해서관리
    ObjectPool objectPool;
    [SerializeField]Transform firePoint;

    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPool>();
    }

    public void Attack()
    {
        if (objectPool == null)
            Debug.LogError("ObjectPool을 찾을 수 없습니다!");
        //각도 조절
        GameObject arrow = objectPool.Get();
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = firePoint.rotation;
        //bullet.GetComponent<Projectile>().Launch(direction);
    }
}
