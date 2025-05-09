using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    private static readonly int isMoving = Animator.StringToHash("isRun");
    private static readonly int isAttack = Animator.StringToHash("isAttack");
    private static readonly int isDead = Animator.StringToHash("isDead");
    private static readonly int isDamaged = Animator.StringToHash("isDamaged");
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Move(Vector2 dir)
    {
        animator.SetBool(isMoving, dir.magnitude > 0.5f);
    }
    public void Damaged()
    {
        animator.SetBool(isDamaged, true);    
    }
    public void Dead()
    {
        animator.SetBool(isDead, true);
    }
    public void IsInvincible()
    {
        animator.SetBool(isDamaged, false);
    }
}
