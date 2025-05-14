using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MonsterStatManager))]
public class MonsterBase : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    SpriteRenderer characterRender;
    public event Action<MonsterBase> OnDeath;
    List<Vector2> path;
    int id;
    string name;
    float exp = 10f;
    protected UnitSoundPlayer soundPlayer;
    public float EXP { get { return exp; } }
    public float ID  { get { return id; } }
    float attackRange;
    Transform target;
    public Transform Target      { get { return target; } }
    public float     AttackRange { get { return attackRange; } }
    float findRange;
    public float FindRange { get { return findRange; } }
    public Vector2 movementDir = Vector2.zero;
    public Vector2 lookDir = Vector2.zero;

    protected MAnimationHandler manationHandler;
    bool isDead = false;
    public bool IsDead { get { return isDead; } }
    bool isDamaged = false;
    public bool IsDamaged { get { return isDamaged; } }
    protected bool isAttack = false;
    public bool IsAttack { get { return isAttack; } }
    protected float attackTime = 0.2f;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    private bool isInvincible = false;
    public bool IsInvincible { get { return isInvincible; } }

    public MonsterStatManager MonsterStatManager { get; private set; }
    private StatusEffectManager statusEffectManager;


    private HPBarUI hpBarUI;
    public MonsterData MonsterData { get; private set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();


        characterRender = GetComponentInChildren<SpriteRenderer>();

        manationHandler = GetComponent<MAnimationHandler>();

        MonsterStatManager = GetComponent<MonsterStatManager>();

        statusEffectManager = GetComponent<StatusEffectManager>();
        soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    public void Start()
    {
        hpBarUI = HealthBarManager.Instance.SpawnHealthBar(transform);
    }

    public void setPath(List<Vector2> _path)
    {
        path = _path;
    }

    public void Init(int _id, string _name, float hp, float _atk, float _def, float speed, float range, float _findRange)
    {
        id = _id;
        name = _name;
        attackRange = range;
        findRange = _findRange;
    }

    public void Init(MonsterData monsterData)
    {
        id = monsterData.ID;
        name = monsterData.Name;
        attackRange = monsterData.AttackRange;
        findRange = monsterData.FindRange;
        MonsterStatManager.Initialize(monsterData);
        MonsterData = monsterData;
    }

    public virtual float Attack()
    {
        movementDir = Vector2.zero;
        lookDir = (target.position - transform.position).normalized;
        Move();

        // StartCoroutine(Timer(MonsterStatManager.monsterStatDic[StatType.AttackSpd].FinalValue, () => isAttack = false));

        return MonsterStatManager.monsterStatDic[StatType.AttackPow].FinalValue;
    }

    IEnumerator Timer(float time, System.Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }

    public void Damaged(float damage)
    {
        // if (isInvincible)
        //     return;
       
        soundPlayer.Play(UnitSoundType.Hit);
        MonsterStatManager.AllDecreaseStatValue(StatType.CurrentHp, damage);
        float curHp = MonsterStatManager.GetFinalValue(StatType.CurrentHp);
        // isInvincible = true;
        manationHandler.Damaged();
        hpBarUI.UpdateFill(curHp, MonsterStatManager.GetFinalValue(StatType.MaxHp));
        // StartCoroutine(Timer(0.2f, () => isInvincible = false));
        if (curHp <= 0)
            Dead();
    }

    private void Dead()
    {
        soundPlayer.Play(UnitSoundType.Die);
        if (statusEffectManager != null)
        {
            statusEffectManager.ClearAllDebuffs();
        }

        manationHandler.Dead();
        isDead = true;
        OnDeath?.Invoke(this);
        hpBarUI.UnLink();

        Destroy(gameObject);
    }


    public void Move()
    {
        Rotate(lookDir);
        Movement(movementDir);
    }

    SoundSource move = null;

    private void Movement(Vector2 direction)
    {
        float moveSpeed = MonsterStatManager.GetFinalValue(StatType.MoveSpeed);
        direction = direction * moveSpeed;
        _rigidbody.velocity = direction;
        if (move == null)
            move = soundPlayer.MakeLoop(UnitSoundType.Move);
        if (direction == Vector2.zero)
            move.LoopStop();
        else
            move.LoopStart();
        manationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ   = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool  isLeft = Mathf.Abs(rotZ) > 90;
        characterRender.flipX = isLeft;
    }

    public bool ShootObstacle()
    {
        Vector2      dir      = (Vector2)(target.position - transform.position);
        float        distance = dir.magnitude;
        int          iL       = 6;
        int          pL       = 7;
        Vector2      boxWidth = new Vector2(0.7f, 0.7f);
        int          mask     = (1 << iL) | (1 << pL);
        RaycastHit2D hit      = Physics2D.BoxCast(transform.position, boxWidth, 0f, dir.normalized, distance, mask);
        // Debug.DrawRay(transform.position, dir.normalized * distance, Color.red);


        if (hit.collider == null)
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
        
        target = _target;
    }

    public void Knockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack && collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HitPart>()?.Damaged(MonsterStatManager.GetFinalValue(StatType.AttackPow));
        }
    }

    public void ApplyDebuff(IDebuffSkill debuffSkill)
    {
        statusEffectManager?.ApplyDebuff(debuffSkill);
    }
}