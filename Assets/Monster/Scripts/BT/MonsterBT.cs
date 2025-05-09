using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBT : MonoBehaviour
{
    private INode root;
    public Transform target;
    // Start is called before the first frame update
    private void Start()
    {
        MonsterBase enemy=GetComponent<MonsterBase>();
        INode live = new EchLiveNode(enemy);
        INode attack =  new EattackNode(enemy);
        INode chase = new EchaseNode(enemy);
        INode check = new EcheckNode(enemy);
        INode findObs=new EchObstacleNode(enemy);
        INode astar = new EaFindNode(enemy);
        INode attackSeq= new SequenceNode(new List<INode> { check,attack });
        INode chaseSeq = new SelectorNode(new List<INode> { attackSeq, chase });
        INode start = new SelectorNode(new List<INode> { new SequenceNode(new List<INode> { findObs, astar }), chaseSeq});
        root = new SequenceNode(new List<INode> {live,start });

    }
    // Update is called once per frame
    void Update()
    {
       
        root?.Evaluate();
    }
}
