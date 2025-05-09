using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetNode : INode
{
    private MonsterBase _enemy;

    public FindTargetNode(MonsterBase enemy)
    {
        _enemy = enemy;
    }
    INode.ENodeState INode.Evaluate()
    {
        Debug.Log("Ȯ�� ������");
        if (_enemy.Target == null)
            return INode.ENodeState.Failure;
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);
        Debug.Log(distance);
        if (distance <= _enemy.FindRange)
        {
            Debug.Log("�� ����");
            return INode.ENodeState.Failure;
        }
        else
        {
            Debug.Log("�� ����");
            return INode.ENodeState.Success;
        }
    }
}
