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
        Debug.Log("확인 실행중");
        if (_enemy.Target == null)
            return INode.ENodeState.Failure;
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);
        Debug.Log(distance);
        if (distance <= _enemy.FindRange)
        {
            Debug.Log("적 존재");
            return INode.ENodeState.Failure;
        }
        else
        {
            Debug.Log("적 없음");
            return INode.ENodeState.Success;
        }
    }
}
