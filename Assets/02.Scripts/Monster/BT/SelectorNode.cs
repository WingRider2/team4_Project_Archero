using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : INode
{
    private List<INode> childs;
    public SelectorNode(List<INode> _childs)
    {
        this.childs = _childs;
    }

    public INode.ENodeState Evaluate()
    {
        if(childs == null)
            return INode.ENodeState.Failure;
        foreach (INode child in this.childs) {
            switch (child.Evaluate()) { 
            case INode.ENodeState.Running:
                    return INode.ENodeState.Running;
            case INode.ENodeState.Success:
                    return INode.ENodeState.Success;
            
            }
        }
        return INode.ENodeState.Failure;
    }
}
