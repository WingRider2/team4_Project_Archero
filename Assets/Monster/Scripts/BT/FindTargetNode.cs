using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetNode : INode
{
    private MonsterBase _enemy;
    private float range;
    public FindTargetNode(MonsterBase enemy,float _range)
    {
        range = _range;
        _enemy = enemy;
    }
    INode.ENodeState INode.Evaluate()
    {
    
        if (_enemy.Target == null)
            return INode.ENodeState.Failure;
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);
      
        if (distance <= range)
        {

            return INode.ENodeState.Success;
        }
        else
        {
      
            return INode.ENodeState.Failure;
        }
    }
}
