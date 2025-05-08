using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer characterRender;
    [SerializeField] private float speed=5f;
    public  Vector2 movementDir= Vector2.zero;
     public Vector2 lookDir= Vector2.zero;
    [SerializeField] protected float attackRange=3f;
    public float AttackRange { get { return attackRange; } }
   
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDir); 
    }
    protected virtual void FixedUpdate()
    {
        Movement(movementDir);
    }
    protected virtual void HandleAction()
    {

    }
    private void Movement(Vector2 direction)
    {
        direction = direction * speed;
        _rigidbody.velocity = direction;
    }
    private void Rotate(Vector2 direction) { 
        float rotZ=Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) < 90;
        characterRender.flipX = isLeft;
    }
}
