using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTab : MonoBehaviour
{
    private TabMenu m_TabMenu;
    private ButtonsSaver m_ButtonsSaver;
    private void Awake()
    {
        m_TabMenu = GetComponent<TabMenu>();
        m_ButtonsSaver = FindAnyObjectByType<ButtonsSaver>();
        m_TabMenu.TabItemSwiched += OnTabItemSwiched;
    }

    private void OnTabItemSwiched(TabItem tabItem)
    {
        CharacterItemChooser.Instance.ResetItemsToCurrent();
        m_ButtonsSaver.BuyButton.gameObject.SetActive(false);
        m_ButtonsSaver.SelectButton.gameObject.SetActive(false);
    }
}
