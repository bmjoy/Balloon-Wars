using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Material m_Material;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;
    private bool m_IsDissolving = false;
    private float m_Fade = 1f;

    void Start()
    {
        m_Material = GetComponent<SpriteRenderer>().material;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_IsDissolving)
        {
            m_Fade -= Time.deltaTime;

            if (m_Fade <= 0f)
            {
                m_Fade = 0f;
                m_IsDissolving = false;
                RestartLevel();
            }

            m_Material.SetFloat("_Fade", m_Fade);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fire"))
        {
            m_Rigidbody.bodyType = RigidbodyType2D.Static;
            m_IsDissolving = true;
        }
    }

    private void Die()
    {
        m_Rigidbody.bodyType = RigidbodyType2D.Static;
        m_Animator.SetTrigger("trap_death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
