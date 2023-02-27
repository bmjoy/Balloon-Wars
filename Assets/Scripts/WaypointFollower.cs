using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_Waypoints;
    private int m_currentWaypointIndex = 0;

    [SerializeField] private float m_Speed = 2f;

    private void Update()
    {
        Vector3 currentWaypointPosition = m_Waypoints[m_currentWaypointIndex].transform.position;

        if (Vector2.Distance(currentWaypointPosition, transform.position) < .1f)
        {
            m_currentWaypointIndex++;
            if (m_currentWaypointIndex >= m_Waypoints.Count)
            {
                m_currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, currentWaypointPosition, Time.deltaTime * m_Speed);
    }
}
