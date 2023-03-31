using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Dart : MonoBehaviour
{
    private PlayerInput m_PlayerInput;
    private InputAction m_TouchHoldAction;
    private InputAction m_TouchPositionAction;

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
        m_TouchHoldAction = m_PlayerInput.actions["TouchHold"];
        m_TouchPositionAction = m_PlayerInput.actions["TouchPosition"];
    }

    private void Start()
    {
        
    }

    void Update()
    {
        foreach (Touch touch in Input.touches) 
        {
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) 
            {
                // The touch is not over any UI element, track the position
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
    }
}
