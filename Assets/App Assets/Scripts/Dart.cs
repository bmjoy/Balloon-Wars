using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    private bool m_HasHit = false;
    [SerializeField] private float m_DartDestroyTime = 3f;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!m_HasHit)
        {
            float angle = Mathf.Atan2(m_RigidBody.velocity.y, m_RigidBody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        m_HasHit = true;
        m_RigidBody.velocity = Vector2.zero;
        m_RigidBody.isKinematic = true;
        StartCoroutine(DestroyDart());
    }

    private IEnumerator DestroyDart()
    {
        yield return new WaitForSeconds(m_DartDestroyTime);
        Destroy(gameObject);
    }
}

