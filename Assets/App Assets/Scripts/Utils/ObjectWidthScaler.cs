using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWidthScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    Vector2 screenSize;

    private void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
    }

    private void Update()
    {
        if(Screen.width != screenSize.x || Screen.height != screenSize.y)
        {
            screenSize.x = Screen.width;
            screenSize.y = Screen.height;
            SpreadSpriteToScreenWidthSize();
        }
    }

    private void Start()
    {
        SpreadSpriteToScreenWidthSize();
    }

    private void SpreadSpriteToScreenWidthSize()
    {
        float targetHeight = mainCamera.orthographicSize * 2f;
        float targetWidth = targetHeight * mainCamera.aspect;
        float scaleX = targetWidth / spriteWidth;
        transform.localScale = new Vector3(scaleX, transform.localScale.y, 1f);
    }
}
