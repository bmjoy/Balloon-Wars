using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public event Action<TabItem> TabItemSwiched;
    private List<TabItem> m_TabItems = new List<TabItem>();
    public TabItem SelectedTabItem{get; private set;} = null;

    public void subscribAsTabItem(TabItem item)
    {
        item.TabItemSelected += OnTabItemSelected;
        m_TabItems.Add(item);
    }

    private void OnTabItemSelected(TabItem tabItem)
    {
        if (tabItem != SelectedTabItem)
        {
            SelectedTabItem?.DeactivateScreen();
            tabItem?.ActivateScreen();
            SelectedTabItem = tabItem;
            TabItemSwiched?.Invoke(tabItem);
        }
    }
}
