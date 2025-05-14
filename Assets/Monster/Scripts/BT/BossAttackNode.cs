using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackNode : INode
{
    private MonsterBase _enemy;
    int num;
    public BossAttackNode(MonsterBase enemy,int _num)
    {
        _enemy = enemy;
        num = _num;
    }
    public INode.ENodeState Evaluate()
    {

        _enemy.movementDir = Vector2.zero;
   
        _enemy.Move();
        MonsterBoss boss = _enemy as MonsterBoss;
        boss.Attack(num);
        return INode.ENodeState.Success;
    }

   
}
