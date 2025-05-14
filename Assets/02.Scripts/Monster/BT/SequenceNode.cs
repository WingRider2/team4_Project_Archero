using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : INode
{
    // Start is called before the first frame update
    private List<INode> childs;
    public SequenceNode(List<INode> _childs)
    {
        this.childs = _childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (childs == null)
            return INode.ENodeState.Failure;
        foreach (INode child in this.childs)
        {
            switch (child.Evaluate())
            {
                case INode.ENodeState.Running:
                    return INode.ENodeState.Running;
                case INode.ENodeState.Success:
                    continue;
                case INode.ENodeState.Failure:
                    return INode.ENodeState.Failure;

            }
        }
        return INode.ENodeState.Success;
    }
}
