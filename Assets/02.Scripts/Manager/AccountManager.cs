using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : Singleton<AccountManager>
{
    public int Gold { get; private set; }

    public event Action<int> OnGoldChanged;

    private void Start()
    {
        Gold = SaveManager.Instance.SaveData.Gold;
    }

    public void AddGold(int gold)
    {
        Gold += gold;
        OnGoldChanged?.Invoke(Gold);
    }

    public void UseGold(int gold)
    {
        if (Gold - gold < 0)
        {
            Debug.LogError("Gold out of range");
            return;
        }

        Gold -= gold;
        OnGoldChanged?.Invoke(Gold);
    }
}