using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBT : MonoBehaviour
{
    private INode root;
    public Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        EnemyController enemy=GetComponent<EnemyController>();
        INode attack =  new EattackNode(enemy);
        INode chase = new EchaseNode(enemy,target);
        INode check = new EcheckNode(enemy,target);
        INode attackSeq= new SequenceNode(new List<INode> { check,attack });
       
        root = new SelectorNode(new List<INode> { attackSeq,chase});
    }
    // Update is called once per frame
    void Update()
    {
        root?.Evaluate();
    }
}
