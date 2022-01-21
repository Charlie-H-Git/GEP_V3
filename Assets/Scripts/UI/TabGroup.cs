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

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    
    //this Class Adds all the tabs to the Tab list
    public void Subscribe(TabButtonScript button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtonScript>();
        }
        tabButtons.Add(button);
    }
    
    
    //This Class changes the Hovered Tab to the TabHover Colour and all other tabs to their normal colour
    public void OnTabEnter(TabButtonScript button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
        
    }

    //This class Calls the reset Tab Class when the mouse exits the buttons bounding box
    public void OnTabExit(TabButtonScript button)
    {
        ResetTabs();
        
    }

    //this class enables the selected tabs corresponding UI page and disable all others
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

    //this class cycles through each tab in the Button list and sets any tab that isn't selected to Idle Colour
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
