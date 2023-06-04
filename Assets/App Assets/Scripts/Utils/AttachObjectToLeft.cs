using System;
using UnityEngine;

public class AttachObjectToLeft : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float positionY = 0f;

    [SerializeField] private float positionX = 0f;

    [SerializeField] private bool isFromTop = false;

    Vector2 screenSize;
    Vector2 oreginalScale;

    private void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        oreginalScale = transform.localScale;
    }

    private void Start()
    {
        scaleObject();
        AttachObjectToLeftSide();
    }

    private void Update()
    {
        if(Screen.width != screenSize.x || Screen.height != screenSize.y)
        {
            screenSize.x = Screen.width;
            screenSize.y = Screen.height;
            scaleObject();
            AttachObjectToLeftSide();
        }
    }

    private void AttachObjectToLeftSide()
    {
        float screenLeft = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
        float screenYAnchor = mainCamera.ScreenToWorldPoint(new Vector3(0f, isFromTop ? Screen.height : 0f, mainCamera.nearClipPlane)).y;
        float targetX = screenLeft  + positionX;
        float targetY = screenYAnchor + positionY;
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

    private void scaleObject()
    {
        float curAspect = mainCamera.aspect;
        float targetAspect = 16f/9f;
        float scale = Math.Min(1f , curAspect / targetAspect);
        transform.localScale = new Vector3(oreginalScale.x * scale, oreginalScale.y * scale, 1);
    }
}
