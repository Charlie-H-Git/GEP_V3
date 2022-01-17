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

public enum PlanetMarkEnum
{
    smallPlanet,
    earthLike,
    Jupiter
}
public class UI_item : MonoBehaviour
{
    [Header("Planets")] 
    public List<GameObject> PlanetMarketReference = new List<GameObject>();

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
    public PlanetMarkEnum planMarkEnum;
    public ItemType itemType;
    public TradeMode tradeMode;
    public PlanetMarket planetMarket;
    private PlayerTradeController _playerTradeController;

    [Header("Integers")] 
    public int tradeAmount;
    public int index;

    [Header("Booleans")] 
    public bool buyMode;
    public bool sellMode;

    private void Awake()
    {
       
        _playerTradeController = GetComponent<PlayerTradeController>();
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
       
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
        StartCoroutine(TradePanel());
    }

    private void OnEnable()
    {
        var planetmarkindex = (int)planMarkEnum;
        print(planetmarkindex);
        PlanetMarket currentMarket = PlanetMarketReference[planetmarkindex].gameObject.GetComponent<PlanetMarket>();
        planetMarket = currentMarket;
        StartCoroutine(TradePanel());
    }

    IEnumerator TradePanel()
    {
       
        //PlanetName.text = 
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
            _playerTradeController.floatWallet = _playerTradeController.floatWallet - ((float)planetMarket.TradeGoodsList[index].Price * tradeAmount);
            planetMarket.PlanetWallet = planetMarket.PlanetWallet + (float)planetMarket.TradeGoodsList[index].Price * tradeAmount;
            //Subtract cost of quantity multiplied by trade amount from player
        }

        if (sellMode)
        {
            planetMarket.TradeGoodsList[index].Quantity = planetMarket.TradeGoodsList[index].Quantity + tradeAmount; 
            _playerTradeController.floatWallet = _playerTradeController.floatWallet + ((float)planetMarket.TradeGoodsList[index].Price * tradeAmount);
            _playerTradeController.activeStorage = _playerTradeController.activeStorage + tradeAmount; 
            planetMarket.PlanetWallet = planetMarket.PlanetWallet - (float)planetMarket.TradeGoodsList[index].Price * tradeAmount;
            //Add cost of quantity multiplied by the trade amount to player
        }
        
        
    }

}
