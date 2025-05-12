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
       
        if(_enemy.Target == null) 
            return INode.ENodeState.Failure;
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.position);
       
        if (distance <=_enemy.AttackRange)
        {
   
            return INode.ENodeState.Success;
        }
        else
        {
    
            return INode.ENodeState.Failure;
        }
    }
}
