using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIRoom : MonoBehaviour
{
    public event Action<string> Clicked;
    [SerializeField] TextMeshProUGUI m_NameTXT;
    public void OnClick()
    {
        Clicked?.Invoke(m_NameTXT.text);
    }
}
