using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : DecoratorNode
{
    public InverterNode(INode child) : base(child) { }
    public override INode.ENodeState Evaluate()
    {
        INode.ENodeState result = child.Evaluate();
        switch (result) {
            case INode.ENodeState.Failure:
                return INode.ENodeState.Success;
            case INode.ENodeState.Success:
                return INode.ENodeState.Failure;
            case INode.ENodeState.Running:
                return INode.ENodeState.Running;
        }
        return result;
    }

   
}
