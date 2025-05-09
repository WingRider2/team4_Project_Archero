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

    public LayerMask target;//���� ���߽� ��뿹��?

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }


    [SerializeField] private float bulletSize = 1; //����ü ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//����ü�߻� �����ð�
    public float Duration { get { return duration; } }
    
    // ���⿡ ���ؼ�

    // ������Ʈ Ǯ�� �Ŀ� �Ŵ����� ���ؼ�����
    ObjectPool objectPool;
    [SerializeField]Transform firePoint;
    [SerializeField] PlayerController player; // ���� �޾ƿ���
    private void Awake()
    {
        objectPool= FindObjectOfType<ObjectPool>();
       
    }
    public void Init(PlayerController playerController)
    {
        player = playerController;

    }
    public void Attack()
    {
        //���� ����
        GameObject arrow = objectPool.Get();
        ProjectileController controller = arrow.GetComponent<ProjectileController>();
        controller.Init(objectPool);
        Vector2 direction = player.lookDirection;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        controller.Launch(direction,speed);
    }
    public virtual void Rotate(bool isLeft)
    {
        //weaponRenderer.flipY = isLeft;//�¿� ȸ��
    }
}
