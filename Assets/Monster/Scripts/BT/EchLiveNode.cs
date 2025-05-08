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
        Debug.Log("생존 확인");
        if(enemy.IsDead)
            return INode.ENodeState.Failure;
        return INode.ENodeState.Success;
    }
}
