using UnityEngine;

public class AttachObjectToLeft : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float positionY = 0f;

    [SerializeField] private float positionX = 0f;

    private void Start()
    {
        AttachObjectToLeftSide();
    }

    private void AttachObjectToLeftSide()
    {
        float objectWidth = transform.localScale.x;
        float objectHeight = transform.localScale.y;

        float screenLeft = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
        float screenBottom = mainCamera.ScreenToWorldPoint(new Vector3(0f, 0f, mainCamera.nearClipPlane)).y;

        float targetX = screenLeft + objectWidth * positionX;
        float targetY = screenBottom + objectHeight * positionY;

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        transform.position = targetPosition;
    }
}
