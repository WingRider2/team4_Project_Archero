using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaponAnimationHandler : MonoBehaviour
{
    private static readonly int ShotTrigger = Animator.StringToHash("ShotTrigger");


    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Shot()
    {
        Debug.Log("�߻�");
        animator.SetTrigger("ShotTrigger");
    }
}
