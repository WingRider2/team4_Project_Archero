using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSimBT : BaseBT
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    protected override void MakeRoot()
    {
        base.MakeRoot();
        INode attackSeq = new SequenceNode(new List<INode> { check, attack });
        INode chaseSeq = new SelectorNode(new List<INode> { attackSeq, chase });
        INode start = new SelectorNode(new List<INode> { new SequenceNode(new List<INode> { findObs, astar }), chaseSeq });
        root = new SequenceNode(new List<INode> { live, start });

    }
}
