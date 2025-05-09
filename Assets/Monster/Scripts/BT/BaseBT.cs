using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBT : MonoBehaviour
{
    protected INode root;
    public Transform target;
    protected MonsterBase enemy;
    protected INode echlive;
    protected INode attack;
    protected INode chase;
    protected INode check;
    protected INode findObs;
    protected INode astar;
    protected INode live;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
        MakeRoot();
    }
    protected virtual void Init()
    {
        MonsterBase enemy = GetComponent<MonsterBase>();
        live = new EchLiveNode(enemy);
        enemy = GetComponent<MonsterBase>();
        echlive = new EchLiveNode(enemy);
      attack = new EattackNode(enemy);
         chase = new EchaseNode(enemy);
       check = new EcheckNode(enemy);
         findObs = new EchObstacleNode(enemy);
       astar = new EaFindNode(enemy);

    }
    protected virtual void MakeRoot()
    {

    }
    // Update is called once per frame
    void Update()
    {
        root?.Evaluate();
    }
}
