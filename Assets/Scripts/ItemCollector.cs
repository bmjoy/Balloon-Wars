using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int m_PineapplesCounter = 0;
    [SerializeField] private TMP_Text m_PineapplesText;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Pineapple"))
        {
            Destroy(collision.gameObject);
            m_PineapplesCounter++;
            m_PineapplesText.text = "Pineapples: " + m_PineapplesCounter;
        }
    }
}
