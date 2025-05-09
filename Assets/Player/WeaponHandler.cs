using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; //���ݼӵ� => ��߻� �ð�
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f; //����ũ��
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f; //���� ����?
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1f; //����ü �ӵ�
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f; //���ݹ���
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;//���� ���߽� ��뿹��?

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
        
    [Header("Ranged Attack Data")]

    [SerializeField] private int bulletIndex;//����ü��ȣ
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1; //����ü ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//����ü�߻� �����ð�
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;//?
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;//�߻�ü�� ����?
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngle;//�߻�ü 2�� ������ �߻� ����
    public float MultipleProjectilesAngle { get { return multipleProjectilesAngle; } }

    [SerializeField] private Color projectileColor;//����ü ����
    public Color ProjectileColor { get { return projectileColor; } }
    // ���⿡ ���ؼ�

    // ������Ʈ Ǯ�� �Ŀ� �Ŵ����� ���ؼ�����
    ObjectPool objectPool;
    [SerializeField]Transform firePoint;

    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPool>();
    }

    public void Attack()
    {
        //���� ����
        GameObject arrow = objectPool.Get();
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = firePoint.rotation;
        //bullet.GetComponent<Projectile>().Launch(direction);
    }
}
