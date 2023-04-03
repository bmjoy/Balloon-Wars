using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private InputManager m_InputManager;
    private Vector2 m_StartPosition;
    private Vector2 m_EndPosition;
    

    private void Awake()
    {
        m_InputManager = InputManager.Instance;
    }

    private void OnEnable() 
    {
        m_InputManager.OnStartTouch += SwipeStart;
        m_InputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable() 
    {
        m_InputManager.OnStartTouch -= SwipeStart;
        m_InputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position)
    {
        m_StartPosition = position;
    }

    private void SwipeEnd(Vector2 position)
    {
        m_EndPosition = position;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Debug.DrawLine(m_StartPosition, m_EndPosition, Color.red, 3f);
    }
}
