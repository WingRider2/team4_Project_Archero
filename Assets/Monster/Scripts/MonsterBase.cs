using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    SpriteRenderer characterRender;
    Animator animator;
    float moveSpeed;
   
    float atk;
    float def;
    protected float currenHP;
    float maxHP;
    string name;
    int id;
    public float ID { get { return id; } }
    float attackRange;
    Transform target;
    public Transform Target { get { return target; } }
    public float AttackRange { get { return attackRange; } }
    public Vector2 movementDir = Vector2.zero;
    public Vector2 lookDir = Vector2.zero;
    bool isDead = false;
    public bool IsDead {  get { return isDead; } } 
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        characterRender = GetComponent<SpriteRenderer>();

    }
    public void Init(int _id,string _name,float hp,float _atk,float _def,float speed,float range)
    {
        id = _id;
        name = _name;
        maxHP = hp;
        currenHP=maxHP;
        def = _def;
        atk =_atk;
        moveSpeed = speed;
        attackRange = range;
    }
    public void Move()
    {
        Rotate(lookDir);
        Movement(movementDir);
    }
    private void Movement(Vector2 direction)
    {
        direction = direction * moveSpeed;
        _rigidbody.velocity = direction;
    }
    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90;
        characterRender.flipX = isLeft;
    }
    public bool ShootObstacle()
    {
        Vector2 dir = (Vector2)(target.position - transform.position);
        float distance = dir.magnitude;
        int iL = 6;
        int pL = 7;
        int mask = (1 << iL) | (1 << pL);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,dir.normalized,distance,mask);
        Debug.DrawRay(transform.position, dir.normalized * distance, Color.red);
        Debug.Log("È®ÀÎ"+hit.collider.tag);
        if (hit == null)
        {
            return false;
        }
        if (hit.collider.CompareTag("Obstacle"))
        {
            return true;
        }
        return false;
    }
    public void SetTarget(Transform _target)
    {
        target= _target;
    }

}
