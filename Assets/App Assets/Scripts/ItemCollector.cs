using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int m_PineapplesCounter = 0;
    [SerializeField] private TMP_Text m_PineapplesText;
    [SerializeField] private AudioSource m_ItemCollectionSound;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Pineapple"))
        {
            m_ItemCollectionSound.Play();
            Destroy(collider.gameObject);
            m_PineapplesCounter++;
            m_PineapplesText.text = "Pineapples: " + m_PineapplesCounter;
        }
    }
}
