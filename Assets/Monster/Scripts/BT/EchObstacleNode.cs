using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchObstacleNode : INode
{
    private MonsterBase _enemy;

    public EchObstacleNode(MonsterBase enemy)
    {
        _enemy = enemy;
    }
    INode.ENodeState INode.Evaluate()
    {
        bool isOb = _enemy.ShootObstacle();
        if (isOb)
        {
            Debug.Log("��ֹ�");
            return INode.ENodeState.Success;
        }
        else
        {
            Debug.Log("����");
            return INode.ENodeState.Failure;
        }
    }
}
