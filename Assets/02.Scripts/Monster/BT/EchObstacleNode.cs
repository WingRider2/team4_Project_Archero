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
    bool isFind = false;
    bool isOb = false;
    float findTimer = 0;
    private const float FindCooldown = 0.5f;
    INode.ENodeState INode.Evaluate()
    {
        if (isFind)
        {
            findTimer += Time.deltaTime;
            if (findTimer >= FindCooldown)
            {
                isFind = false;
                findTimer = 0f;
            }
        }
     
        if (!isFind)
        {
            isOb = _enemy.ShootObstacle();
        }
        if (isOb)
        {
      
            isFind = true;
            return INode.ENodeState.Success;
        }
        else
        {
     
            return INode.ENodeState.Failure;
        }
    }
}
