using UnityEngine;
using Photon.Pun;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource m_ItemCollectionSound;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Pineapple"))
        {
            m_ItemCollectionSound.Play();
            PhotonNetwork.Destroy(collider.gameObject);
        }
    }
}
