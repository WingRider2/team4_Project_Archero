using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchaseNode : INode
{
    private MonsterBase _enemy;

    public EchaseNode(MonsterBase enemy)
    {
        _enemy = enemy;
      
    }
    public INode.ENodeState Evaluate()
    {
       
        if (_enemy.Target == null)
            return INode.ENodeState.Failure;
        Vector2 dir=(_enemy.Target.position-_enemy.transform.position).normalized;
        _enemy.movementDir = dir;
        _enemy.lookDir = dir;
        _enemy.Move();
        return INode.ENodeState.Running;
    }
}
