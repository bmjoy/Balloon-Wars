using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int m_PineapplesCounter = 0;
    [SerializeField] private TMP_Text m_PineapplesText;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Pineapple"))
        {
            Destroy(collider.gameObject);
            m_PineapplesCounter++;
            m_PineapplesText.text = "Pineapples: " + m_PineapplesCounter;
        }
    }
}
