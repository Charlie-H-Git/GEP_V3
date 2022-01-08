using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum TradeMode 
{
    Buy,
    sell
}
public class UI_item : MonoBehaviour
{
    [Header("Text Boxes")]
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text quantityText;
    
    [Header("Buttons")]
    public TMP_InputField inputField;
    public Button tradeButton;
    public Button plusButton;
    public Button minusButton;
    
    [Header("Script References")]
    public ItemType itemType;
    public TradeMode tradeMode;
    public PlanetMarket planetMarket;

    [Header("Integers")] 
    public int tradeAmount;
    public int index;

    [Header("Booleans")] 
    public bool buyMode;
    public bool sellMode;

    private void Awake()
    {
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        StartCoroutine(TradePanel());
    }

    private void Start()
    {
        index = (int) itemType;
        switch (tradeMode)
        {
            case TradeMode.Buy:
                tradeButton.GetComponentInChildren<TMP_Text>().text = "Buy";
                sellMode = false;
                buyMode = true;
                break;
            case TradeMode.sell:
                tradeButton.GetComponentInChildren<TMP_Text>().text = "Sell";
                buyMode = false;
                sellMode = true;
                break;
        }
        tradeButton.onClick.AddListener(TradeBtnClick);
        
    }

    private void OnEnable()
    {
        StartCoroutine(TradePanel());
    }

    IEnumerator TradePanel()
    {
        
        nameText.text = itemType.ToString();
        costText.text = ("Cost = " + planetMarket.TradeGoodsList[index].Price.ToString("C"));
        quantityText.text = "Quantity = " + planetMarket.TradeGoodsList[index].Quantity;
        int.TryParse(inputField.text, out int result);
        tradeAmount = result;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TradePanel());
    }
    
    void TradeBtnClick()
    {
        //Debug.Log($"trade amount " + tradeAmount + $" sum " + planetMarket.TradeGoodsList[index].Quantity);
        if (buyMode)
        {
            planetMarket.TradeGoodsList[index].Quantity = planetMarket.TradeGoodsList[index].Quantity - tradeAmount;
            //Subtract cost of quantity multiplied by trade amount from player
        }

        if (sellMode)
        {
            planetMarket.TradeGoodsList[index].Quantity = planetMarket.TradeGoodsList[index].Quantity + tradeAmount; 
            //Add cost of quantity multiplied by the trade amount to player
        }
        
        
    }

}
