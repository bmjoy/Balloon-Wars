using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDart : MonoBehaviour
{
    public Vector2 ShootDirection{get; set;}
    private List<GameObject> m_Points;
    private PhotonView m_PhotonView;
    private Vector2 m_DefaultDartDirection;
    [SerializeField] private GameObject m_DartPrefab;
    [SerializeField] private GameObject m_PointPrefab;
    [SerializeField] [Range(10,25)] private float m_ShotForce = 16;
    [SerializeField] private Transform m_ShotPoint;
    [SerializeField] [Range(30,70)] private int m_NumOfProjectionPoints = 50;
    [SerializeField] [Range(0.01f, 0.05f)] private float m_SpaceBetweenPoints = 0.025f;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(m_PhotonView.IsMine)
        {
            if(context.performed || context.started)
            {
                ShootDirection = (context.ReadValue<Vector2>()) * -1;
                Debug.Log($"shoot direction: {ShootDirection}");
                transform.right = ShootDirection;
                UpdateProjectionPointsLocation();
            }

            if(context.started)
            {
                Debug.Log("Shoot started");
                setPointsActiveState(true);
            }

            if(context.canceled)
            {
                Debug.Log("Shoot ended");
                Shoot();
                transform.right = m_DefaultDartDirection;
                setPointsActiveState(false);
            }
        }
    }

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(m_PhotonView.IsMine)
        {
            m_DefaultDartDirection = transform.right;
            InitializeProjectionPoints();
        }
    }

    private void Shoot()
    {
        GameObject newDart = PhotonNetwork.Instantiate(m_DartPrefab.name, m_ShotPoint.position, m_ShotPoint.rotation);
        newDart.GetComponent<Transform>().right = ShootDirection;
        newDart.GetComponent<Rigidbody2D>().velocity = transform.right * m_ShotForce;
    }

    private void InitializeProjectionPoints()
    {
        m_Points = new List<GameObject>();
        for(int i=0; i < m_NumOfProjectionPoints; i++)
        {
            GameObject currentPoint = PhotonNetwork.Instantiate(m_PointPrefab.name, m_ShotPoint.position, Quaternion.identity);
            currentPoint.transform.SetParent(gameObject.transform);
            m_Points.Add(currentPoint);
        }
        setPointsActiveState(active: false);
    }

    private void setPointsActiveState(bool active)
    {
        foreach (GameObject point in m_Points)
        {
            point.SetActive(active);
        }
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 startingVelocity = ShootDirection.normalized * m_ShotForce;
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