using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneGameManager : MonoBehaviour
{
    public int IngameGetCoin { get; private set; }


    public void AddGold(int coin)
    {
        IngameGetCoin += coin;
    }
}