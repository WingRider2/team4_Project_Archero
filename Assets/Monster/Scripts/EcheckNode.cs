using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcheckNode : INode
{
    private EnemyController _enemy;
    private Transform target;
    public EcheckNode(EnemyController enemy, Transform target)
    {
        _enemy = enemy;
        this.target = target;
    }
    INode.ENodeState INode.Evaluate()
    {
        Debug.Log("Ȯ�� ������");
        if(target == null) 
            return INode.ENodeState.Failure;
        float distance = Vector2.Distance(_enemy.transform.position, target.position);
        Debug.Log(distance);
        if (distance <=_enemy.AttackRange)
        {
            Debug.Log("Ž�� ����");
            return INode.ENodeState.Success;
        }
        else
        {
            Debug.Log("Ž�� ����");
            return INode.ENodeState.Failure;
        }
    }
}
