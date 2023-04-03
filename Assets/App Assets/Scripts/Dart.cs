using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Dart : MonoBehaviour//, IPunObservable
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
        if(m_PhotonView.IsMine)
        {
            m_HasHit = true;
            m_RigidBody.velocity = Vector2.zero;
            m_RigidBody.isKinematic = true;
            StartCoroutine(StartDestroyDart());
        }
    }

    private IEnumerator StartDestroyDart()
    {
        if(m_PhotonView.IsMine)
        {
            yield return new WaitForSeconds(m_DartDestroyTime);
            m_Animator.SetTrigger("fade");
        }
    }

    public void DestroyDart()
    {
        if(m_PhotonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    // [PunRPC]
    // private void UpdateSpriteColor(Color color)
    // {
    //     GetComponentInChildren<SpriteRenderer>().color = color;
    // }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsReading)
    //     {
    //         // If this is the owner of the object, send the flipX value over the network
    //         Color updatedColor = (Color)stream.ReceiveNext();
    //         GetComponentInChildren<SpriteRenderer>().color = updatedColor;
    //     }
    //     else
    //     {
    //         // Write the current color to the network
    //         Color currentColor = GetComponentInChildren<SpriteRenderer>().color;
    //         stream.SendNext(currentColor);
    //     }
    // }
}

