using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillZone : MonoBehaviour, IPoolObject
{
    public float time = 2f;
    [SerializeField] PoolType poolType;
    [SerializeField] private int poolSize = 20;

    protected MWeaponHandler mWeaponHandler;
    public float duration=2f;
    public float attackDuration=1f;
    public int damage = 20;
    private SpriteRenderer spriteRenderer;
    private Color startColor = new Color(1, 0, 0, 0.2f);
    private Color endColor = new Color(1, 0, 0, 0.7f);
    private Color attackColor = new Color(1, 0, 0, 1f);
    public void Init(Vector2 pos, MWeaponHandler weaponHandler)
    {
        timer = 0;
   
        gameObject.transform.position = pos;
        mWeaponHandler = weaponHandler;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;
        isAttack = false;
        isReady = true;

    }
    bool isAttack;
    bool isReady=false;

    public GameObject GameObject => gameObject;
    public PoolType PoolType => poolType;
    public int PoolSize => poolSize;

    protected ObjectPoolManager mPoolManager;

    public void Start()
    {
        mPoolManager = ObjectPoolManager.Instance;
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttack && collision.CompareTag("Player"))
        {
            mPoolManager.ReturnObject(this);

            collision.gameObject.GetComponent<HitPart>()?.Damaged(1);

        }
    }
    public float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        if (!isReady)
            return;
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer/duration);
        spriteRenderer.color=Color.Lerp(startColor, endColor, t);
        if (timer >= duration) {
            isAttack = true;
        }
        if (timer > duration + attackDuration)
        {
            spriteRenderer.color=attackColor;
            mPoolManager.ReturnObject(this);
            isReady = false;

        }
    }
}
