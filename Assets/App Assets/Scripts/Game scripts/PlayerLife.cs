using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Unity.VisualScripting;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    private Material m_Material;
    private Animator m_Animator;
    private Animator m_ScreenAnimator;
    private Rigidbody2D m_Rigidbody;
    private SpawnPlayers m_PlayerSpawner;
    private PhotonView m_PhotonView;
    private bool m_IsDissolving = false;
    private float m_Fade = 1f;
    
    [SerializeField] private GameObject m_PlayerDart;
    [SerializeField] private GameObject m_NameLabel;
    [SerializeField] private AudioSource m_SharpTrapSound;
    [SerializeField] private AudioSource m_BurnSound;

    private void Awake()
    {
         m_PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        m_Material = GetComponent<SpriteRenderer>().material;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerSpawner = FindAnyObjectByType<SpawnPlayers>();
        m_ScreenAnimator = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_PhotonView.IsMine)
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
                StartCoroutine(GameOver(0f));
            }

            m_Material.SetFloat("_Fade", m_Fade);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (m_PhotonView.IsMine)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (m_PhotonView.IsMine)
        {
            if (collider.gameObject.CompareTag("Fire"))
            {
                m_BurnSound.Play();
                m_Rigidbody.bodyType = RigidbodyType2D.Static;
                m_PhotonView.RPC("DisablePlayerObjects", RpcTarget.All);
                m_IsDissolving = true;
            }
        }
    }

    public void Die()
    {
        m_SharpTrapSound.Play();
        m_PhotonView.RPC("DisablePlayerObjects", RpcTarget.All);
        m_Rigidbody.bodyType = RigidbodyType2D.Static;
        m_Animator.SetTrigger("trap_death");
        StartCoroutine(GameOver(0.3f));
    }       

    public IEnumerator GameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_ScreenAnimator.SetTrigger("LostIn");
        PhotonNetwork.Destroy(this.gameObject);
    }

    [PunRPC]
    private void DisablePlayerObjects()
    {
        m_PlayerDart.SetActive(false);
        m_NameLabel.SetActive(false);
    }

    private void RestartLevel()
    {
        if(m_PhotonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
            m_PlayerSpawner.RespawnPlayer();
        }
    }
}
