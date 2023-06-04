using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabItem : MonoBehaviour, IPointerClickHandler
{
    private TabMenu m_TabMenu;
    private Color ActiveColor = new Color32(r: 181, g: 113, b: 54, a: 255);
    private Color DisabledColor = new Color32(r: 181, g: 113, b: 54, a: 0);
    [SerializeField] GameObject m_TabScreen;
    [SerializeField] bool m_IsSelected = false;
    void Start()
    {
        m_TabMenu = GetComponentInParent<TabMenu>();
        m_TabMenu.subscribAsTabItem(this);
        m_TabScreen.SetActive(false);
        if(m_IsSelected)
        {
            m_TabMenu.OnTabItemSelected(this);
        }
    }

    internal void ActivateScreen()
    {
        GetComponent<Image>().color = ActiveColor;
        m_TabScreen.SetActive(true);
    }

    internal void DeactivateScreen()
    {
        GetComponent<Image>().color = DisabledColor;
        m_TabScreen.SetActive(false);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        m_TabMenu.OnTabItemSelected(this);
    }
}
