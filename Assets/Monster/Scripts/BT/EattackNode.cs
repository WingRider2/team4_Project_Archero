using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EattackNode : INode
{
    private MonsterBase _enemy;
    public EattackNode(MonsterBase enemy)
    {
        _enemy = enemy;
    }
    public INode.ENodeState Evaluate()
    {

        _enemy.Attack();


        _enemy.movementDir = Vector2.zero;
        _enemy.Move();
        return INode.ENodeState.Running;
    }
}
