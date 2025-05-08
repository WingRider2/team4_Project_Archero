using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EIdleNode : INode
{
    private EnemyController _enemy;
    public EIdleNode(EnemyController enemy)
    {
        _enemy = enemy;
    }
    public INode.ENodeState Evaluate()
    {
        _enemy.movementDir=Vector2.zero;
        _enemy.lookDir=Vector2.zero;
        return INode.ENodeState.Success;
    }
}
