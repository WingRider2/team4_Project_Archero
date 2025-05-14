using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownNode : DecoratorNode
{
    private float cooldownTime;
    private float lastExecutionTime = -Mathf.Infinity;
    public CoolDownNode(INode child,float cooldownTime) : base(child)
    {
        this.cooldownTime = cooldownTime;
    }
    
    public override INode.ENodeState Evaluate()
    {
        if (Time.time - lastExecutionTime >= cooldownTime) { 
            var result = child.Evaluate();
            if(result==INode.ENodeState.Success || result == INode.ENodeState.Failure)
            {
                lastExecutionTime=Time.time;
            }
        
            return result;
        }
        return INode.ENodeState.Success;
    }

    
}
