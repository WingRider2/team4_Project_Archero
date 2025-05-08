using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcheckNode : INode
{
    private MonsterBase _enemy;

    public EcheckNode(MonsterBase enemy)
    {
        _enemy = enemy;
    }
    INode.ENodeState INode.Evaluate()
    {
        Debug.Log("Ȯ�� ������");
        if(_enemy.Target == null) 
            return INode.ENodeState.Failure;
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);
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
