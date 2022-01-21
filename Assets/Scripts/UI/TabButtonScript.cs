using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TabButtonScript : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;
    
    //This script uses the event system in unity to check for mouse position relative to UI components
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
    
    //This class add the gameobject to the tab list in the TabGroup 
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }
    
}
