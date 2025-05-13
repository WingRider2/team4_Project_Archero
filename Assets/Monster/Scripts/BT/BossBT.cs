using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBT : BaseBT
{
    protected override void Init()
    {
        
        base.Init();
    }
    protected override void MakeRoot()
    {
        base.MakeRoot();
        INode attack1 = new BossAttackNode(enemy, 1);
        INode attack2 = new BossAttackNode(enemy, 2);
        INode teleport = new ETeleportNode(enemy);
        INode attackSeq = new SequenceNode(new List<INode> { check, attack1 });
        INode chaseSel = new SelectorNode(new List<INode> { attackSeq, attack2 });
        INode findTSeq = new SequenceNode(new List<INode> { findTar, teleport });
        INode findObS = new SequenceNode(new List<INode> {  findObs, teleport });
        INode selR = new SelectorNode(new List<INode> {findObS, chaseSel, findTSeq });
    
        root = new SequenceNode(new List<INode> { live ,chaseSel });

    }
}
