using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETeleportNode : INode
{
    MonsterBase enemy;
    public ETeleportNode(MonsterBase enemy)
    {
        
    }
    public INode.ENodeState Evaluate()
    {
        if(enemy == null)
        return INode.ENodeState.Failure;

        MonsterBoss boss = enemy as MonsterBoss;
        boss.Teleport(enemy.Target.position);
        return INode.ENodeState.Success;

    }

  
}
