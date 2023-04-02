using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Dart : MonoBehaviour, IPunObservable
{
    private Rigidbody2D m_RigidBody;
    private PhotonView m_PhotonView;
    private bool m_HasHit = false;
    [SerializeField] private float m_DartDestroyTime = 3f;

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
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
            StartCoroutine(DestroyDart());
        }
    }

    private IEnumerator DestroyDart()
    {
        if(m_PhotonView.IsMine)
        {
            yield return new WaitForSeconds(m_DartDestroyTime);
            StartCoroutine(fadeDartOut());
        }
    }

    private IEnumerator fadeDartOut()
    {
        if(m_PhotonView.IsMine)
        {
            Color dartColor = GetComponentInChildren<Renderer>().material.color;
            float fadeAmount = 0.1f;
            for(int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.1f);
                Color newColor = new Color(dartColor.r, dartColor.g, dartColor.b, 1 - fadeAmount * i);
                GetComponentInChildren<Renderer>().material.color = newColor;

                m_PhotonView.RPC("UpdateSpriteColor", RpcTarget.Others, newColor);
            }
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void UpdateSpriteColor(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            // If this is the owner of the object, send the flipX value over the network
            Color updatedColor = (Color)stream.ReceiveNext();
            GetComponentInChildren<SpriteRenderer>().color = updatedColor;
        }
        else
        {
            // Write the current color to the network
            Color currentColor = GetComponentInChildren<SpriteRenderer>().color;
            stream.SendNext(currentColor);
        }
    }
}

