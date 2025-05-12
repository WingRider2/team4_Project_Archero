using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchLiveNode : INode
{
    MonsterBase enemy;
    public EchLiveNode(MonsterBase enemy)
    {
        this.enemy = enemy;
    }

    public INode.ENodeState Evaluate()
    {
       
        if(enemy.IsDead||enemy.IsDamaged||enemy.IsAttack)
            return INode.ENodeState.Failure;
 
        return INode.ENodeState.Success;
    }
}
