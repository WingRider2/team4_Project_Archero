using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponHandler : MonoBehaviour
{
    // ���⿡ ���ؼ�
    // ������Ʈ Ǯ�� �Ŀ� �Ŵ����� ���ؼ�����
    ObjectPool objectPool;
    [SerializeField]Transform firePoint;

    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPool>();
    }

    public void Attack()
    {
        if (objectPool == null)
            Debug.LogError("ObjectPool�� ã�� �� �����ϴ�!");
        //���� ����
        GameObject arrow = objectPool.Get();
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = firePoint.rotation;
        //bullet.GetComponent<Projectile>().Launch(direction);
    }
}
