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
    protected INode findTar;
    protected INode idle;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
        MakeRoot();
    }
    protected virtual void Init()
    {
        enemy = GetComponent<MonsterBase>();
        live = new EchLiveNode(enemy);
       
        echlive = new EchLiveNode(enemy);
        attack = new CoolDownNode(new EattackNode(enemy), enemy.MonsterStatManager.monsterStatDic[StatType.AttackSpd].FinalValue);
         chase = new EchaseNode(enemy);
       check = new FindTargetNode(enemy, enemy.AttackRange);
         findObs = new EchObstacleNode(enemy);
       astar = new EaFindNode(enemy);
        findTar = new InverterNode(new FindTargetNode(enemy, enemy.FindRange));
            
        idle = new EIdleNode(enemy);
        

    }
    protected virtual void MakeRoot()
    {

    }
    // Update is called once per frame
    bool isStart = false;
    float timer = 0;
    private const float FindCooldown = 1f;
    void Update()
    {
        if (!isStart)
        {
            timer += Time.deltaTime;
            if (timer >= FindCooldown)
            {
                isStart = true;
            }
            return;
        }
        
        root?.Evaluate();
       
    }
}
