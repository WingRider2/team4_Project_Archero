using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EaFindNode : INode
{
    private MonsterBase _enemy;
    private Astar astar;
    private Vector2 lastPos = Vector2.zero;
    private float Threshold = 0.5f;
    List<Vector2> path;
    public EaFindNode(MonsterBase enemy)
    {
        _enemy = enemy;
       astar = new Astar();
    }
    bool isMoving = false;
    bool isFind = false;
    float findTimer = 0;
    private const float FindCooldown = 1f;
    public INode.ENodeState Evaluate()
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

        if (_enemy.Target == null)
            return INode.ENodeState.Failure;
        if (!isFind) {
          
            isFind = true;
            path = null;
            if (path == null || Vector2.Distance(lastPos, _enemy.transform.position) > Threshold)
            {
                path = astar.FindPath(_enemy.transform.position, _enemy.Target.position);

            }
        }
    
        if(path==null||path.Count<2)
            return INode.ENodeState.Failure;
        Vector2 nowPos=path[0];
        Vector2 targetPos=path[1];
        Vector2 dir = (targetPos -(Vector2) _enemy.transform.position).normalized;
        _enemy.movementDir = dir;
        _enemy.lookDir = dir;
        _enemy.Move();
      
        if(Vector2.Distance(_enemy.transform.position,targetPos) < Threshold)
        {
            path.RemoveAt(0);
        }
        return INode.ENodeState.Running;
    }
}
