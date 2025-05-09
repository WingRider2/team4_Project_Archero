using System;
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
}
[Serializable]
public class StageData
{
    public Tilemap FloorTilemap;
    public Tilemap WallTilemap;
    public Tilemap ColliderTilemap;
    public int MonsterSpawnCount;
    public int ObstacleSpawnCount;

}