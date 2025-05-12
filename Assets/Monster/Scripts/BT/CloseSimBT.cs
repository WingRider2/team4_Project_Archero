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
        INode chaseSel = new SelectorNode(new List<INode> { attackSeq, chase });
        INode findTSeq = new SequenceNode(new List<INode> { findTar, idle });
        INode findObS = new SequenceNode(new List<INode> {  findObs, astar });
        INode selR = new SelectorNode(new List<INode> { findTSeq,findObS, chaseSel });
    
        root = new SequenceNode(new List<INode> { live ,selR });

    }
}
