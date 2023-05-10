using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    private PhotonView m_PhotonView;

    private Animator m_Animator;
    private bool m_HasHit = false;
    [SerializeField] private float m_DartDestroyTime = 3f;

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!m_HasHit && m_PhotonView.IsMine)
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
        StartCoroutine(StartDestroyDart());
    }

    private IEnumerator StartDestroyDart()
    {
        yield return new WaitForSeconds(m_DartDestroyTime);
        m_Animator.SetTrigger("fade");
    }

    [PunRPC]
    private void DartHitBalloonRPC()
    {
        if(m_PhotonView.IsMine)
            PhotonNetwork.Destroy(this.gameObject);
    }

    public void DestroyDart()
    {
        Destroy(this.gameObject);
    }
}

