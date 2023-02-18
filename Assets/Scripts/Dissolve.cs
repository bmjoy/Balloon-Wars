using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disolve : MonoBehaviour
{
    private Material m_Material;
    private bool m_IsDissolving = false;
    private float m_Fade = 1f;

    void Start()
    {
        m_Material = GetComponent<SpriteRenderer>().material;
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
            }

            m_Material.SetFloat("_Fade", m_Fade);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Pineapple"))
        {
            m_IsDissolving = true;
        }
    }
}
