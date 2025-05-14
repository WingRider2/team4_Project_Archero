using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapManager : SceneOnlyManager<MapManager>
{
    [SerializeField] private Tilemap floorMap;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private Tilemap colliderMap;
    [SerializeField] private Tilemap doorTilemap;
    [SerializeField] private Tilemap playerSpawnTilemap;
    [SerializeField] private Tilemap bossSpawnTilemap;

    [SerializeField] private GameObject[] obstacleObjects;
    [SerializeField] private GameObject doorPrefabs;

    [SerializeField] private TilemapData[] tilemapDatas;
    [SerializeField] private GameObject testPrefab;


    [SerializeField] private GameObject playerObject;

    public Door CurrentDoor { get; private set; }
    private GameObject currentDoorObject;
    public Tilemap FloorMap => floorMap;

    public List<Vector3> MonsterSpawnPositions { get; private set; } = new List<Vector3>();

    public int currentStage = 0;
    List<GameObject> instanceObstacleObjects = new List<GameObject>();

    public ChapterData CurrentChapterData { get; private set; }

    protected override void Awake()
    {
    }

    void Start()
    {
        CurrentChapterData = TableManager.Instance.GetTable<StageTable>().GetDataByID(GameManager.Instance.SelectedChapter);
        GenerateMap();
    }


    public void GenerateMap()
    {
        if (CurrentChapterData == null)
        {
            print($"Chapter not found");
            return;
        }

        instanceObstacleObjects.ForEach(Destroy);
        StageData   stageData   = CurrentChapterData.StageDatas[currentStage];
        TilemapData tilemapData = tilemapDatas[Random.Range(0, tilemapDatas.Length)];
        GenerateTile(floorMap, tilemapData.FloorTilemap);
        GenerateTile(wallMap, tilemapData.WallTilemap);
        GenerateTile(colliderMap, tilemapData.ColliderTilemap);
        GenerateTile(doorTilemap, tilemapData.DoorTilemap);
        GenerateTile(playerSpawnTilemap, tilemapData.PlayerSpawnTilemap);
        GenerateTile(bossSpawnTilemap, tilemapData.BossSpawnTilemap);


        SpawnPlayer();
        SpawnDoors();
        GenerateObstacle(stageData);
        CameraController.Instance.MapUpdate();
        QuestManager.Instance.UpdateCurrentCount(QuestConditionType.Challenge, 1);
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

        tilemap.CompressBounds();
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
            if (tileIndex.Count == vaildPositions.Count)
                break;

            tileIndex.Add(Random.Range(0, vaildPositions.Count));
        }

        List<int> sortedIndices = tileIndex.ToList();
        sortedIndices.Sort((a, b) => b.CompareTo(a)); // 내림차순
        foreach (var index in sortedIndices)
        {
            GameObject go = Instantiate(obstacleObjects[Random.Range(0, obstacleObjects.Length)]);
            go.transform.position = floorMap.CellToWorld(vaildPositions[index]) + new Vector3(0.5f, 0.5f);
            vaildPositions.RemoveAt(index);
            instanceObstacleObjects.Add(go);
        }

        SpawnMonster(stageData, vaildPositions);
    }

    List<Vector3Int> GenerateSpawnArea()
    {
        List<Vector3Int> result = new();
        BoundsInt        bounds = floorMap.cellBounds;

        for (int x = bounds.xMin + 1; x < bounds.xMax - 1; x++)
        {
            for (int y = bounds.yMin + 1; y < bounds.yMax - 1; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (wallMap.HasTile(pos)) continue;
                if (playerSpawnTilemap.HasTile(pos)) continue;
                if (!floorMap.HasTile(pos)) continue;
                if (bossSpawnTilemap.HasTile(pos)) continue;

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

        MonsterManager.Instance.makeMonList(MonsterSpawnPositions, 1);
        if (stageData.IsBossStage)
            MonsterManager.Instance.MakeBossMonster(SpawnBoss());
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

    private Vector3 SpawnBoss()
    {
        foreach (var pos in bossSpawnTilemap.cellBounds.allPositionsWithin)
        {
            if (!bossSpawnTilemap.HasTile(pos))
                continue;

            return bossSpawnTilemap.CellToWorld(pos);
        }

        return Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (floorMap == null || wallMap == null) return;

        BoundsInt bounds = floorMap.cellBounds;

        for (int x = bounds.xMin + 1; x < bounds.xMax - 1; x++)
        {
            for (int y = bounds.yMin + 1; y < bounds.yMax - 1; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (!floorMap.HasTile(cellPos)) continue;
                if (wallMap.HasTile(cellPos)) continue;

                Vector3 worldPos = floorMap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);

                // ✅ 그리기
                Gizmos.color = new Color(0f, 1f, 0f, 0.1f); // 연한 녹색
                Gizmos.DrawCube(worldPos, new Vector3(1f, 1f, 0.1f));
            }
        }
    }

    protected override void OnDestroy()
    {
    }
}