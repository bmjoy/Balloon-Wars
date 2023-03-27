using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSaw : MonoBehaviour
{
    [SerializeField] private float m_Speed = 2f;

    private void Update()
    {
        transform.Rotate(0, 0, 360 * m_Speed * Time.deltaTime);
    }
}
