using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer doorSpriteRenderer;

    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closeSprite;

    [SerializeField] private BoxCollider2D doorCollider;


    /// <summary>
    /// 스테이지 클리어시 해당 함수 호출
    /// </summary>
    public void DoorControl(bool isOpen)
    {
        doorSpriteRenderer.sprite = isOpen ? openSprite : closeSprite;
        doorCollider.isTrigger = isOpen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.ReStart();
        }
    }
}