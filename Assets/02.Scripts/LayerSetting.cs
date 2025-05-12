using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerSetting : MonoBehaviour
{
    Tilemap floorTilemap;
    SpriteRenderer targetRenderer;

    private void Awake()
    {
        targetRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        floorTilemap = MapManager.Instance.FloorMap;
        Vector3Int cellPos = floorTilemap.WorldToCell(transform.position);
        targetRenderer.sortingOrder = Mathf.RoundToInt(cellPos.y) * -1;
    }
}