using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class CameraController : SceneOnlyManager<CameraController>
{
    [SerializeField] private Transform target;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int maxZoom;
    private float camHalfWidth;
    private float camHalfHeight;

    private Bounds mapBounds;

    private Camera mainCamera;

    protected override void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        MapUpdate();
    }

    public void MapUpdate()
    {
        camHalfHeight = mainCamera.orthographicSize;
        camHalfWidth = camHalfHeight * mainCamera.aspect;
        mapBounds = tilemap.localBounds;
    }

    private void LateUpdate()
    {
        if (target == null) return;


        Vector3 desiredPosition = target.position;

        float minX = mapBounds.min.x + camHalfWidth;
        float maxX = mapBounds.max.x - camHalfWidth;

        float minY = mapBounds.min.y + camHalfHeight;
        float maxY = mapBounds.max.y - camHalfHeight;

        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // private void HandleZoom()
    // {
    //     float scroll = Input.GetAxis("Mouse ScrollWheel");
    //     if (scroll != 0f)
    //     {
    //         mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scroll * 5f, 3f, maxZoom);
    //         camHalfHeight = mainCamera.orthographicSize;
    //         camHalfWidth = camHalfHeight * mainCamera.aspect;
    //     }
    // }
    protected override void OnDestroy()
    {
    }
}