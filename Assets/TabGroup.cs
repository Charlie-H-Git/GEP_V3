using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButtonScript> tabButtons;
    public TabButtonScript selectedTab;
    
    
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public void Subscribe(TabButtonScript button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtonScript>();
        }
        tabButtons.Add(button);
    }

    private void Start()
    {
        
    }

    public void OnTabEnter(TabButtonScript button)
    {
        ResetTabs();
        button.background.color = tabHover;
    }

    public void OnTabExit(TabButtonScript button)
    {
        ResetTabs();
        //button.background.color = tabIdle;
    }

    public void OnTabSelected(TabButtonScript button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
    }

    public void ResetTabs()
    {
        foreach (TabButtonScript button in tabButtons)
        {
            button.background.color = tabIdle;
        }
    }
}
