using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : INode
{
    protected INode child;
    public DecoratorNode(INode child)
    {
        this.child = child;
    }

    public abstract INode.ENodeState Evaluate();
}
