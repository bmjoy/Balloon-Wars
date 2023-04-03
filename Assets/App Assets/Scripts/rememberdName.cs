using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rememberdName : MonoBehaviour
{
    private TextMeshProUGUI m_NameText;

    private void Start()
    {
        TextMeshProUGUI NameText = GetComponent<TextMeshProUGUI>();
        string savedName = PlayerPrefs.GetString("USERNAME");
        NameText.text = string.IsNullOrEmpty(savedName)? "ERROR" : savedName; 
    }
}
