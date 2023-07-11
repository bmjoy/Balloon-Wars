using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSaver : MonoBehaviour
{

    public Button BuyButton{get; private set;}
    public Button SelectButton{get; private set;}
    
    private void Awake()
    {
        BuyButton = GameObject.FindGameObjectWithTag("BuyButton").GetComponent<Button>();
        SelectButton = GameObject.FindGameObjectWithTag("SelectButton").GetComponent<Button>();
    }

    private void Start() {
        BuyButton.gameObject.SetActive(false);
        SelectButton.gameObject.SetActive(false);
    }
}
