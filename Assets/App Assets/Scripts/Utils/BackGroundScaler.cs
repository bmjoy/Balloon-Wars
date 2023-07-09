using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private Vector2 screenSize;
    private float spriteWidth;
    private float spriteHeight;

    private void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
        spriteHeight = spriteRenderer.sprite.bounds.size.y;
    }

    private void Update()
    {
        if(Screen.width != screenSize.x || Screen.height != screenSize.y)
        {
            screenSize.x = Screen.width;
            screenSize.y = Screen.height;
            SpreadSpriteToScreenSize();
        }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpreadSpriteToScreenSize();
    }

    private void SpreadSpriteToScreenSize()
    {
        float targetHeight = mainCamera.orthographicSize * 2f;
        float targetWidth = targetHeight * mainCamera.aspect;
        float scaleX = targetWidth / spriteWidth;
        float scaleY = targetHeight / spriteHeight;
        float scale = Mathf.Max(scaleX, scaleY);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}