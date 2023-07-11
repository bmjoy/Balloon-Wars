using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Skins.Catagories m_Catagory;
    [SerializeField] private int m_ItemCost;
    [SerializeField] private string m_ItemName;
    [SerializeField] private TextMeshProUGUI m_CostText;
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private GameObject m_CostBar;
    private Button m_ItemButton;
    private ButtonsSaver m_ButtonsSaver;
    private bool m_IsBought = false;
    private bool m_IsSelected = false;
    private void Awake() 
    {
        m_CostText.SetText(m_ItemCost.ToString());
        m_NameText.SetText(m_ItemName);
        m_ItemButton = GetComponent<Button>();        
        m_IsBought = CheckIfItemBought();
        m_ItemButton.onClick.AddListener(OnClick);
        m_CostBar.SetActive(!m_IsBought);
        m_ButtonsSaver = FindAnyObjectByType<ButtonsSaver>();
    }

    private bool CheckIfItemSelected()
    {
        switch(m_Catagory)
        {
            case Skins.Catagories.Character:
                return CharacterItemChooser.Instance.CurCharacter.ToString() == m_ItemName;
            case Skins.Catagories.Hat:
                return CharacterItemChooser.Instance.CurHat.ToString() == m_ItemName;
            case Skins.Catagories.Weapon:
                return CharacterItemChooser.Instance.CurWeapon.ToString() == m_ItemName;
            case Skins.Catagories.Shirt:
                return CharacterItemChooser.Instance.CurShirt.ToString() == m_ItemName;
            case Skins.Catagories.Pants:
                return CharacterItemChooser.Instance.CurPants.ToString() == m_ItemName;
            default:
                return false;
        }
    }

    private bool CheckIfItemBought()
    {
        if(m_ItemCost == 0)
        {
            return true;
        }
        //todo: check if item is bought in playfab
        return false;
    }

    private void OnClick()
    {
        m_IsSelected = CheckIfItemSelected();
        m_ButtonsSaver.BuyButton.gameObject.SetActive(!m_IsBought);
        m_ButtonsSaver.SelectButton.gameObject.SetActive(!m_IsSelected && m_IsBought);
        CharacterItemChooser.Instance.changeItem(m_Catagory, m_ItemName);
        m_ButtonsSaver.BuyButton.onClick.RemoveAllListeners();
        m_ButtonsSaver.BuyButton.onClick.AddListener(OnItemBuy);
        m_ButtonsSaver.SelectButton.onClick.RemoveAllListeners();
        m_ButtonsSaver.SelectButton.onClick.AddListener(selectItem);
    }

    public void OnItemBuy()
    {
        // if have enough coins
        m_IsBought = true;
        m_ButtonsSaver.BuyButton.gameObject.SetActive(false);
        m_ButtonsSaver.SelectButton.gameObject.SetActive(true);
        m_CostBar.SetActive(false);
    }

    public void selectItem()
    {
        CharacterItemChooser.Instance.SelectItem(m_Catagory, m_ItemName);
        m_ButtonsSaver.SelectButton.gameObject.SetActive(false);
    }
}