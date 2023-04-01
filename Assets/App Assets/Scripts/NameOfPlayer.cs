using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameOfPlayer : MonoBehaviour
{
    private static readonly string GUEST = "Guest";
    private TextMeshProUGUI m_NameText;

    private void Awake()
    {
        m_NameText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        string username = PlayerPrefs.GetString("USERNAME");
        m_NameText.SetText(!string.IsNullOrEmpty(username)? username : GUEST);
    }
}
