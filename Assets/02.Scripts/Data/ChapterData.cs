﻿using System;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class ChapterData
{
    [SerializeField] public string Name;
    public int ID;

    [Header("Stage Data")]
    public List<StageData> StageDatas;

    [FormerlySerializedAs("RewardDatas")]
    [Header("Reward Data")]
    public RewardData RewardData;
}

[Serializable]
public class StageData
{
    public int MonsterSpawnCount;
    public int ObstacleSpawnCount;
    public bool IsBossStage;
}

[Serializable]
public class TilemapData
{
    public Tilemap FloorTilemap;
    public Tilemap WallTilemap;
    public Tilemap ColliderTilemap;
    public Tilemap DoorTilemap;
    public Tilemap PlayerSpawnTilemap;
    public Tilemap BossSpawnTilemap;
}