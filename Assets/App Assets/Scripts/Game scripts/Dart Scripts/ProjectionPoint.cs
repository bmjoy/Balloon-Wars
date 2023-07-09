using Photon.Pun;
using UnityEngine;

public class ProjectionPoint : MonoBehaviour
{
    private PhotonView m_PhotonView;
    private Renderer m_Renderer;

    private void Awake() 
    {
        m_PhotonView = GetComponent<PhotonView>();
        m_Renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        if (!m_PhotonView.IsMine)
        {
            m_Renderer.enabled = false;
        }
    }
}
