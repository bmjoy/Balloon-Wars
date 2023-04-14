using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public List<TabItem> m_TabItems;
    private TabItem selectedTabItem = null;

    public void subscribAsTabItem(TabItem item)
    {
        if(m_TabItems == null)
        {
            m_TabItems = new List<TabItem>();
        }
        m_TabItems.Add(item);
    }

    public void OnTabItemSelected(TabItem tabItem)
    {
        if (tabItem != selectedTabItem)
        {
            selectedTabItem?.DeactivateScreen();
            tabItem?.ActivateScreen();
            selectedTabItem = tabItem;
        }
    }
}
