using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int ID;
    public List<StatData> statData = new List<StatData>();
}