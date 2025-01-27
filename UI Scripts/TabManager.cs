using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    // Inspired by game dev guide

    public List<TabImage> tabs;
    public TabImage selectedTab;
    // Start is called before the first frame update
    
    public void Subscribe(TabImage tab)
    {
        if(tabs == null)
        {
            tabs = new List<TabImage>();
        }

        tabs.Add(tab);
    }

    public void OnTabEnter(TabImage tab)
    {
        ResetTabs();
        if (tab != selectedTab)
        {
            tab.SetHover();
        }
    }

    public void OnTabExit(TabImage tab)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabImage tab)
    {
        if (selectedTab != null && tab != selectedTab)
        {
            selectedTab.OnDeselected();
        }
        selectedTab = tab;
        ResetTabs();
        tab.OnSelected();

    }

    private void ResetTabs()
    {
        foreach(TabImage tab in tabs)
        {
            if(selectedTab != null && tab == selectedTab)
            {
                continue;
            }
            tab.SetIdle();
        }
    }
}
