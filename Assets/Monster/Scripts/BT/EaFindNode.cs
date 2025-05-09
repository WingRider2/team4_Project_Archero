using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EaFindNode : INode
{
    private MonsterBase _enemy;
    private Astar astar;
    public EaFindNode(MonsterBase enemy)
    {
        _enemy = enemy;
        astar = new Astar();
    }
    public INode.ENodeState Evaluate()
    {
        Debug.Log("장애물 존재!");
        _enemy.movementDir = new Vector2(0,0);
         _enemy.lookDir = new Vector2(0, 0);
        _enemy.Move();
        return INode.ENodeState.Running;
        // if (_enemy.Target == null)
        //     return INode.ENodeState.Failure;
        // List<Vector2> path = astar.FindPath(_enemy.transform.position, _enemy.Target.position);
        //if(path != null )
        //     return INode.ENodeState.Failure;
        // Vector2 dir = (path[0]-_enemy.transform.position).normalized;
        // _enemy.movementDir = dir;
        // _enemy.lookDir = dir;
        // _enemy.Move();
        // return INode.ENodeState.Running;
    }
}
