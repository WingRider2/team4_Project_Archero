using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HPBarUI : MonoBehaviour, IPoolObject
{
    [SerializeField] PoolType poolType;
    [SerializeField] private int poolSize = 15;

    [SerializeField] RectTransform barRect;
    [SerializeField] Image fillImage;
    [SerializeField] Vector3 offset;


    public GameObject GameObject => gameObject;
    public PoolType   PoolType   => poolType;
    public int        PoolSize   => poolSize;

    Transform target;
    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Initialize(Transform targetTrans)
    {
        target = targetTrans;
        transform.SetParent(HealthBarManager.Instance.hpBarCanvas.transform);
    }

    public void UpdatePosion()
    {
        if (target == null)
            return;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position + offset);
        barRect.position = screenPos;
    }


    public void UpdateFill(float cur, float max)
    {
        fillImage.fillAmount = Mathf.Clamp01(cur / max);
    }

    public void UnLink()
    {
        target = null;
        HealthBarManager.Instance.DespawnHealthBar(this);
        fillImage.fillAmount = 1f;
        barRect.position = Vector3.zero;
    }
}