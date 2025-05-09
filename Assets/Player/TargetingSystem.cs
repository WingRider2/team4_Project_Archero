using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public GameObject target;

    public void findTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");// �Ŀ� ���� ���� Ȥ�� ����Ҷ� ����;

        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        target = nearest;
    }
}
