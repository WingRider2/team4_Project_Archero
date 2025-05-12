using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EIdleNode : INode
{
    private MonsterBase _enemy;
    public EIdleNode(MonsterBase enemy)
    {
        _enemy = enemy;
    }
    public INode.ENodeState Evaluate()
    {
        _enemy.movementDir=Vector2.zero;
        _enemy.lookDir=Vector2.zero;
        _enemy.Move();
        return INode.ENodeState.Running;
    }
}
