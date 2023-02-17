using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_Player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_Player.position.x, m_Player.position.y, transform.position.z);
    }
}
