using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerLife : MonoBehaviour
{
    private Material m_Material;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;

    private PhotonView m_View;
    private bool m_IsDissolving = false;
    private float m_Fade = 1f;
    
    [SerializeField] private AudioSource m_SharpTrapSound;
    [SerializeField] private AudioSource m_BurnSound;

    private void Awake()
    {
         m_View = GetComponent<PhotonView>();
    }

    private void Start()
    {
        m_Material = GetComponent<SpriteRenderer>().material;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (m_View.IsMine)
        {
            UpdateDissolvingState();
        }
    }

    private void UpdateDissolvingState()
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
        if (m_View.IsMine)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (m_View.IsMine)
        {
            if (collider.gameObject.CompareTag("Fire"))
            {
                m_BurnSound.Play();
                m_Rigidbody.bodyType = RigidbodyType2D.Static;
                m_IsDissolving = true;
            }
        }
    }

    private void Die()
    {
        m_SharpTrapSound.Play();
        m_Rigidbody.bodyType = RigidbodyType2D.Static;
        m_Animator.SetTrigger("trap_death");
    }

    private void RestartLevel()
    {
        if(m_View.IsMine)
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
        }
    }
}
