using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : SceneOnlyManager<HealthBarManager>
{
    public Canvas hpBarCanvas;

    [SerializeField] private GameObject healthBarPrefab;

    private List<HPBarUI> activeBars = new();


    private void LateUpdate()
    {
        foreach (var bar in activeBars)
        {
            bar.UpdatePosion();
        }
    }

    public HPBarUI SpawnHealthBar(Transform targetTransform)
    {
        HPBarUI bar = ObjectPoolManager.Instance.GetObject(PoolType.HealthBar).GetComponent<HPBarUI>();
        bar.Initialize(targetTransform);
        activeBars.Add(bar);
        return bar;
    }

    public void DespawnHealthBar(IPoolObject bar)
    {
        activeBars.Remove(bar.GameObject.GetComponent<HPBarUI>());
        ObjectPoolManager.Instance.ReturnObject(bar);
    }
}