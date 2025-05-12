using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Tilemap floorMap;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private Tilemap colliderMap;
    [SerializeField] private Tilemap doorTilemap;
    [SerializeField] private Tilemap playerSpawnTilemap;

    [SerializeField] private GameObject[] obstacleObjects;
    [SerializeField] private GameObject doorPrefabs;

    [SerializeField] private TilemapData[] tilemapDatas;
    [SerializeField] private GameObject testPrefab;


    [SerializeField] private GameObject playerObject;


    public Door CurrentDoor { get; private set; }
    private GameObject currentDoorObject;
    public Tilemap FloorMap => floorMap;

    public List<Vector3> MonsterSpawnPositions { get; private set; }

    private void Awake()
    {
    }

    void Start()
    {
        GenerateMap(TableManager.Instance.GetTable<StageTable>().GetDataByID(1));
    }


    public void GenerateMap(ChapterData chapterData)
    {
        StageData   stageData   = chapterData.StageDatas[Random.Range(0, chapterData.StageDatas.Count)];
        TilemapData tilemapData = tilemapDatas[Random.Range(0, tilemapDatas.Length)];
        GenerateTile(floorMap, tilemapData.FloorTilemap);
        GenerateTile(wallMap, tilemapData.WallTilemap);
        GenerateTile(colliderMap, tilemapData.ColliderTilemap);
        GenerateTile(doorTilemap, tilemapData.DoorTilemap);
        GenerateTile(playerSpawnTilemap, tilemapData.PlayerSpawnTilemap);
        SpawnPlayer();
        SpawnDoors();
        GenerateObstacle(stageData);
    }

    private void GenerateTile(Tilemap tilemap, Tilemap dataTilemap)
    {
        tilemap.ClearAllTiles();
        var floor  = dataTilemap;
        var bounds = floor.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            var tile = floor.GetTile(pos);
            if (tile != null)
                tilemap.SetTile(pos, tile);
        }
    }

    /// <summary>
    /// 장애물을 랜덤으로 생성하는 메서드
    /// </summary>
    /// <param name="stageData"></param>
    private void GenerateObstacle(StageData stageData)
    {
        List<Vector3Int> vaildPositions = GenerateSpawnArea();
        if (vaildPositions.Count == 0)
            return;

        HashSet<int> tileIndex = new HashSet<int>();
        while (tileIndex.Count < stageData.ObstacleSpawnCount)
        {
            tileIndex.Add(Random.Range(0, vaildPositions.Count));
        }

        foreach (var index in tileIndex)
        {
            GameObject go = Instantiate(obstacleObjects[Random.Range(0, obstacleObjects.Length)]);
            go.transform.position = floorMap.CellToWorld(vaildPositions[index]) + new Vector3(0.5f, 0.5f);
            vaildPositions.RemoveAt(index);
        }

        SpawnMonster(stageData, vaildPositions);
    }

    List<Vector3Int> GenerateSpawnArea()
    {
        List<Vector3Int> result = new();
        BoundsInt        bounds = floorMap.cellBounds;

        for (int x = bounds.xMin + 1; x < bounds.xMax - 1; x++)
        {
            for (int y = bounds.yMin + 1; y < bounds.yMax - 2; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (wallMap.HasTile(pos)) continue;
                if (playerSpawnTilemap.HasTile(pos)) continue;
                if (!floorMap.HasTile(pos)) continue;

                result.Add(pos);
            }
        }

        return result;
    }

    private void SpawnMonster(StageData stageData, List<Vector3Int> vaildPositions)
    {
        HashSet<int> tileIndex = new HashSet<int>();
        MonsterSpawnPositions.Clear();
        while (tileIndex.Count < stageData.MonsterSpawnCount)
        {
            tileIndex.Add(Random.Range(0, vaildPositions.Count));
        }

        foreach (var index in tileIndex)
        {
            MonsterSpawnPositions.Add(floorMap.CellToWorld(vaildPositions[index]) + new Vector3(0.5f, 0.5f));
        }
    }

    private void SpawnDoors()
    {
        foreach (var pos in doorTilemap.cellBounds.allPositionsWithin)
        {
            if (!doorTilemap.HasTile(pos))
                continue;

            Vector3 worldPos = doorTilemap.CellToWorld(pos);
            if (currentDoorObject == null)
            {
                GameObject go = Instantiate(doorPrefabs, worldPos, Quaternion.identity);
                CurrentDoor = go.GetComponent<Door>();
                currentDoorObject = go;
            }
            else
            {
                currentDoorObject.transform.position = worldPos;
            }

            break;
        }

        if (CurrentDoor != null)
        {
            CurrentDoor.DoorControl(false);
        }
    }

    private void SpawnPlayer()
    {
        foreach (var pos in playerSpawnTilemap.cellBounds.allPositionsWithin)
        {
            if (!playerSpawnTilemap.HasTile(pos))
                continue;

            Vector3 worldPos = playerSpawnTilemap.CellToWorld(pos);
            playerObject.transform.position = worldPos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (floorMap == null || wallMap == null) return;

        BoundsInt bounds = floorMap.cellBounds;

        for (int x = bounds.xMin + 1; x < bounds.xMax - 1; x++)
        {
            for (int y = bounds.yMin + 1; y < bounds.yMax - 2; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (!floorMap.HasTile(cellPos)) continue;
                if (wallMap.HasTile(cellPos)) continue;

                Vector3 worldPos = floorMap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);

                // ✅ 그리기
                Gizmos.color = new Color(0f, 1f, 0f, 0.3f); // 연한 녹색
                Gizmos.DrawCube(worldPos, new Vector3(1f, 1f, 0.1f));
            }
        }
    }
}