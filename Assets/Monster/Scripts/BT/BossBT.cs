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
        INode attack1 = new CoolDownNode(new BossAttackNode(enemy, 1), enemy.MonsterStatManager.monsterStatDic[StatType.AttackSpd].FinalValue);
        INode attack2 = new CoolDownNode(new BossAttackNode(enemy, 2), enemy.MonsterStatManager.monsterStatDic[StatType.AttackSpd].FinalValue);
        INode attack3 = new CoolDownNode(new BossAttackNode(enemy, 3), 5f);
    
        INode teleport = new ETeleportNode(enemy);
        INode checkClose = new InverterNode(new FindTargetNode(enemy, 0.7f));
        INode attackSeq = new SequenceNode(new List<INode> { check,attack1,checkClose,chase });
        INode chaseSel = new SelectorNode(new List<INode> { attackSeq, attack2 });
        INode findTSeq = new SequenceNode(new List<INode> { findTar, attack3 });
        INode findObS = new SequenceNode(new List<INode> { findObs, attack3 });
        INode selR = new SelectorNode(new List<INode> {findObS, findTSeq, chaseSel });
     
        root = new SequenceNode(new List<INode> { live ,selR });

    }
}
