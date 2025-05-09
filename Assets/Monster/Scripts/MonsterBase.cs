using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    SpriteRenderer characterRender;

    float moveSpeed;
   
    protected float atk;
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

    protected MAnimationHandler manationHandler;
    bool isDead = false;
    public bool IsDead {  get { return isDead; } }
    bool isDamaged = false;
    public bool IsDamaged { get { return isDamaged; } }
    protected bool isAttack = false;
    public bool IsAttack { get { return isAttack; } }
    protected float attackTime=0.2f;
    private float coolTime = 2.0f;
    private Vector2 knockback=Vector2.zero;
    private float knockbackDuration = 0.0f;
    private bool isInvincible = false;
    public bool IsInvincible { get { return isInvincible; } }
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
       
        characterRender = GetComponent<SpriteRenderer>();
        manationHandler= GetComponent<MAnimationHandler>();
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
    public virtual float Attack()
    {
        isAttack = true;
        Debug.Log("공격" + atk);
        StartCoroutine(Timer(coolTime, () => isAttack = false));
        return atk;
    }
    IEnumerator Timer(float time,System.Action onComplete)
    {
        yield  return new WaitForSeconds(time);
        onComplete();

    }
    private void Damaged(float damage)
    {
        currenHP -= damage;
        Damaged(currenHP);
        isInvincible = true;
        manationHandler.Damaged();
        StartCoroutine(Timer(1.0f,() => isInvincible = false));
        if (currenHP <= 0)
            Dead();
    }
    private void Dead()
    {
        manationHandler.Dead();
        isDead = true;
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
       manationHandler.Move(direction);
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
        Debug.Log("확인"+hit.collider.tag);
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
    public void Knockback(Transform other,float power,float duration)
    {
        knockbackDuration= duration;
        knockback=-(other.position - transform.position).normalized*power;   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttack)
        {
            //플레이어 데미지 소환.
        }
    }
}
