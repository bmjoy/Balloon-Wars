using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDart : MonoBehaviour
{
    private Camera m_MainCamera;
    private Vector2 m_TouchPosition;
    private Vector2 m_DartPosition;
    private Vector2 m_DartDirection;
    private Vector2 m_DefaultDartDirection;

    [SerializeField] private GameObject m_DartPrefab;
    [SerializeField] private float m_ShotForce;
    [SerializeField] private Transform m_ShotPoint;

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void Start()
    {
        m_DefaultDartDirection = transform.right;
    }

    private void Update()
    {
        AcceptTouchInputs();
    }

    private void AcceptTouchInputs()
    {
        foreach (Touch touch in Input.touches) 
        {
            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                if (!IsPointerOverUIObject()) 
                {
                    m_TouchPosition = Utils.ScreenToWorld(m_MainCamera, touch.position);
                    UpdateDartPointDirection();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (!IsPointerOverUIObject()) 
                {
                    m_TouchPosition = Utils.ScreenToWorld(m_MainCamera, touch.position);
                    Shoot();
                    transform.right = m_DefaultDartDirection;
                }
            }
        }
    }

    private void UpdateDartPointDirection()
    {
        UpdateBowDirection();
        transform.right = m_DartDirection;
    }

    private void UpdateBowDirection()
    {
        m_DartPosition = transform.position;
        m_DartDirection = m_TouchPosition - m_DartPosition;
    }

    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void Shoot()
    {
        GameObject newDart = Instantiate(m_DartPrefab, m_ShotPoint.position, m_ShotPoint.rotation);
        newDart.GetComponent<Transform>().right = m_DartDirection;
        newDart.GetComponent<Rigidbody2D>().velocity = transform.right * m_ShotForce;
    }
}
