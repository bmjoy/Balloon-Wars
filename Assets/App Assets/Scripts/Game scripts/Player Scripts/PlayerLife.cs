using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Unity.VisualScripting;
using System.Collections;
using Photon.Realtime;

public class PlayerLife : MonoBehaviour
{
    private Animator m_Animator;
    private Animator m_ScreenAnimator;
    private Rigidbody2D m_Rigidbody;
    private SpawnPlayers m_PlayerSpawner;
    private PhotonView m_PhotonView;
    private GameObject[] m_ControllButtons;
    PhotonRoomInfo m_PhotonRoomInfo;
    [SerializeField] private GameObject m_PlayerDart;
    [SerializeField] private GameObject m_NameLabel;
    [SerializeField] private AudioSource m_SharpTrapSound;
    [SerializeField] private AudioSource m_BurnSound;
    [SerializeField] private AudioSource m_FallSound; 

    private void Awake()
    {
         m_PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerSpawner = FindAnyObjectByType<SpawnPlayers>();
        m_ScreenAnimator = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<Animator>();
        m_PhotonRoomInfo = FindObjectOfType<PhotonRoomInfo>();
        m_ControllButtons = GameObject.FindGameObjectsWithTag("ControllButton");
        if(m_PhotonView.IsMine)
        {
            GetComponent<BalloonHolder>().BallonsFinishd += OutOfBalloonsDie;
            m_PhotonRoomInfo.PlayerWon += handleWin; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (m_PhotonView.IsMine)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                m_SharpTrapSound.Play();
                TrapDie();
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
                TrapDie();
            }
        }
    }

    public void TrapDie()
    {
        m_PhotonView.RPC("StartDie", RpcTarget.All);
        StartCoroutine(GameOver(delay: 0.3f, didWon: false));
    }

    public void OutOfBalloonsDie()
    {
        m_PhotonView.RPC("setPlayerFalling", RpcTarget.All);
        m_FallSound.Play();
        deActivateControllButtons();
        StartCoroutine(GameOver(delay: 2.5f, didWon: false));
    }

    private void deActivateControllButtons()
    {
        foreach (GameObject button in m_ControllButtons)
        {
            button.SetActive(false);
        }
    }

    public IEnumerator GameOver(float delay, bool didWon)
    {
        yield return new WaitForSeconds(delay);
        m_ScreenAnimator.SetTrigger(didWon? "WinIn" : "LostIn");
        PhotonNetwork.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        removePlayerFromGame();
    }

    private void removePlayerFromGame()
    {
        if (m_PhotonRoomInfo != null)
        {
            m_PhotonRoomInfo.RemovePlayerFromGame(m_PhotonView.Owner);
        }
    }

    [PunRPC]
    private void StartDie()
    {
        m_PlayerDart.SetActive(false);
        m_NameLabel.SetActive(false);
        m_Rigidbody.bodyType = RigidbodyType2D.Static;
        m_Animator.SetTrigger("Die");
    }

    [PunRPC]
    private void setPlayerFalling()
    {
        removePlayerFromGame();
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        m_PlayerDart.SetActive(false);
        m_NameLabel.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().mass = 3;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
    }

    private void handleWin(Player winningPlayer)
    {
        if(m_PhotonView.Owner == winningPlayer)
        {
            Debug.Log("You Won!");
            m_Animator.SetTrigger("trap_death");
            StartCoroutine(GameOver(delay: 0, didWon: true));
        }
    }
}
