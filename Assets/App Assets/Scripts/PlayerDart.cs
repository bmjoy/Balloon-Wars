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
    private List<GameObject> m_Points;

    [SerializeField] private GameObject m_DartPrefab;
    [SerializeField] private GameObject m_PointPrefab;
    [SerializeField] private float m_ShotForce;
    [SerializeField] private Transform m_ShotPoint;
    [SerializeField] private int m_NumOfProjectionPoints;
    [SerializeField] private float m_SpaceBetweenPoints;

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    private void Start()
    {
        m_DefaultDartDirection = transform.right;
        InitializeProjectionPoints();
    }

    private void Update()
    {
        AcceptTouchInputs();
        UpdateProjectionPointsLocation();
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

    private void InitializeProjectionPoints()
    {
        m_Points = new List<GameObject>();

        for(int i=0; i < m_NumOfProjectionPoints; i++)
        {
            m_Points.Add(Instantiate(m_PointPrefab, m_ShotPoint.position, Quaternion.identity));
        }
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 startingVelocity = m_DartDirection.normalized * m_ShotForce;
        Vector2 startingPosition = (Vector2)m_ShotPoint.position;
        Vector2 acceleration = 0.5f * Physics2D.gravity;
        float tSquare = t*t;
        Vector2 position = startingPosition + (startingVelocity * t) + (acceleration * tSquare);
        
        return position;
    }

    private void UpdateProjectionPointsLocation()
    {
        for (int i=0; i < m_NumOfProjectionPoints; i++)
        {
            m_Points[i].transform.position = PointPosition(i * m_SpaceBetweenPoints);
        }
    }
}
