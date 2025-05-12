using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] SpriteRenderer doorSpriteRenderer;
    [SerializeField] Sprite openSprite;
    [SerializeField] private BoxCollider2D collider;

    /// <summary>
    /// 스테이지 클리어시 해당 함수 호출
    /// </summary>
    public void OpenDoor()
    {
        doorSpriteRenderer.sprite = openSprite;
        collider.enabled = false;
    }
}