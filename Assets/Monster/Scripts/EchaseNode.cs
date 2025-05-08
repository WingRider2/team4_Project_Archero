using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchaseNode : INode
{
    private EnemyController _enemy;
    private Transform target;
    public EchaseNode(EnemyController enemy, Transform target)
    {
        _enemy = enemy;
        this.target = target;
    }
    public INode.ENodeState Evaluate()
    {
        Debug.Log("���� ������");
        if (target == null)
            return INode.ENodeState.Failure;
        Vector2 dir=(target.position-_enemy.transform.position).normalized;
        _enemy.movementDir = dir;
        _enemy.lookDir = dir;
        return INode.ENodeState.Running;
    }
}
