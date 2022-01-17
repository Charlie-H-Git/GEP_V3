using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButtonScript> tabButtons;
    public TabButtonScript selectedTab;

    public List<GameObject> ui_Pages;
    public int panelIndex;

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
    
    
    public void OnTabEnter(TabButtonScript button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
        
    }

    public void OnTabExit(TabButtonScript button)
    {
        ResetTabs();
        //button.background.color = tabIdle;
    }

    public void OnTabSelected(TabButtonScript button)
    {
        Debug.Log("TAB SELECTED");
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < ui_Pages.Count; i++)
        {
            if (i == index)
            {
                ui_Pages[i].SetActive(true);
            }
            else
            {
                ui_Pages[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButtonScript button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab )
            {
                continue;
            }
            button.background.color = tabIdle;
        }
    }
    
}
