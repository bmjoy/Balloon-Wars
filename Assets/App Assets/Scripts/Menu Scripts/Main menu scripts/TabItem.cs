using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabItem : MonoBehaviour, IPointerClickHandler
{
    private TabMenu m_TabMenu;
    [SerializeField] private Color ActiveColor;
    [SerializeField] private Color DisabledColor;
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
