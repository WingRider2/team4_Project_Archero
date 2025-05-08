using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EattackNode : INode
{
    private EnemyController _enemy;
    public EattackNode(EnemyController enemy)
    {
        _enemy = enemy;
    }
    public INode.ENodeState Evaluate()
    {
        Debug.Log("АјАн!");
        _enemy.movementDir = Vector2.zero;
        return INode.ENodeState.Success;
    }
}
