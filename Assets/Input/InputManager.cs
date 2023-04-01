using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviourSingleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position);
    public event EndTouch OnEndTouch;
    #endregion

    private PlayerControls m_PlayerControls;
    private Camera m_MainCamera;

    private void Awake() 
    {
        m_PlayerControls = new PlayerControls();
        m_MainCamera = Camera.main;
    }

    private void OnEnable() 
    {
        m_PlayerControls.Enable();
    }

    private void OnDisable() 
    {
        m_PlayerControls.Disable();
    }

    void Start()
    {
        m_PlayerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        m_PlayerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null && !IsPointerOverUIObject())
        {
            OnStartTouch(Utils.ScreenToWorld(m_MainCamera, m_PlayerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null && !IsPointerOverUIObject())
        {
            OnEndTouch(Utils.ScreenToWorld(m_MainCamera, m_PlayerControls.Touch.PrimaryPosition.ReadValue<Vector2>()));
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(m_MainCamera, m_PlayerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
