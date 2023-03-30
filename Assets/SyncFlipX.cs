using UnityEngine;
using Photon.Pun;

public class SyncFlipX : MonoBehaviour, IPunObservable
{
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(m_SpriteRenderer.flipX);
        }
        else
        {
            m_SpriteRenderer.flipX = (bool)stream.ReceiveNext();
        }
    }
}
