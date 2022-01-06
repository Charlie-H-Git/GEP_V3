using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_item : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text quantityText;
    public PlanetMarket planetMarket;
    public ItemType itemtype;
    

    private void Start()
    {
        int index = (int) itemtype;
        //planetMarket = GetComponent<PlanetMarket>();
        nameText.text = itemtype.ToString();
        costText.text = ("Cost = " + planetMarket.TradeGoodsList[index].Price.ToString("C"));
        quantityText.text = "Quantity = " + planetMarket.TradeGoodsList[index].Quantity;
    }
    
}
