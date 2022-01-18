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
    [Header("Planets")]
    public GameObject[] planetGameObjectArray;
    
    [Header("Text Boxes")]
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text quantityText;
    public TMP_Text playerQuant;
   
    [Header("Buttons // UI Items")]
    public TMP_InputField inputField;
    public Button tradeButton;
    public Button plusButton;
    public Button minusButton;
    private Image _UIitemBack;

    [Header("Script References")]
    //public PlanetMarkEnum planMarkEnum;
    public ItemType itemType;
    public TradeMode tradeMode;
    private PlanetMarket planetMarket;
    public PlayerTradeController _playerTradeController;

    [Header("Integers")] 
    public int tradeAmount;
    public int index;

    [Header("Booleans")] 
    public bool buyMode;
    public bool sellMode;
    

    private void Awake()
    {
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        _UIitemBack = gameObject.GetComponent<Image>();
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
        minusButton.onClick.AddListener(IncrementDown);
        plusButton.onClick.AddListener(IncrementUp);
        StartCoroutine(TradePanel());
    }

    private void OnEnable()
    {
        foreach (var p in planetGameObjectArray)
        {
            if (p.GetComponent<PlanetMarket>().trading)
            {
                planetMarket = p.GetComponent<PlanetMarket>();
                //Debug.Log("planet market assigned");
                StartCoroutine(TradePanel());
            }
        }
        
    }

    private Color baseColour = new Color32(28, 37, 89, 255);
    
    IEnumerator TradeRejection()
    {
        _UIitemBack.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);
        
        _UIitemBack.color = baseColour;

        yield return new WaitForSeconds(0.1f);

    }
    IEnumerator TradePanel()
    {
        TradeCap();
        nameText.text = itemType.ToString();
        playerQuant.text = "Player Stock = " + _playerTradeController.PlayerInventory[index].Quantity.ToString();
        costText.text = ("Cost = " + planetMarket.TradeGoodsList[index].Price.ToString("C"));
        quantityText.text = "Quantity = " + planetMarket.TradeGoodsList[index].Quantity;
        int.TryParse(inputField.text, out int result);
        tradeAmount = result;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(TradePanel());
    }

    
    //TODO add limiter on tradeamount by capping input at cargo capacity / remaining cargo capacity
    private void TradeCap()
    {
        int capacity = _playerTradeController.storageCapacity;
        int remainder = _playerTradeController.storageCapacity - _playerTradeController.activeStorage;
        int.TryParse(inputField.text, out int result);
        if (buyMode && result > remainder)
        {
            inputField.text = remainder.ToString();
        }

        if (sellMode && result > capacity)
        {
            inputField.text = capacity.ToString();
        }
    }

    /// <summary>
    /// TODO: Add limit and rejection for transactions bigger than players total balance do the same for planet wallet too
    /// </summary>

    void IncrementDown()
    {
        int.TryParse(inputField.text, out int result);
        result--;
        inputField.text = result.ToString();
    }

    void IncrementUp()
    {
        int.TryParse(inputField.text, out int result);
        result++;
        inputField.text = result.ToString();
    }
    
    void TradeBtnClick()
    {
        //Debug.Log($"trade amount " + tradeAmount + $" sum " + planetMarket.TradeGoodsList[index].Quantity);
        if (buyMode)
        {
            if (planetMarket.TradeGoodsList[index].Price * tradeAmount < _playerTradeController.floatWallet && planetMarket.TradeGoodsList[index].Quantity >= tradeAmount)
            {
                _playerTradeController.traded = true;
                planetMarket.TradeGoodsList[index].Quantity = planetMarket.TradeGoodsList[index].Quantity - tradeAmount;
                _playerTradeController.floatWallet = _playerTradeController.floatWallet - ((float)planetMarket.TradeGoodsList[index].Price * tradeAmount);
                planetMarket.PlanetWallet = planetMarket.PlanetWallet + (float)planetMarket.TradeGoodsList[index].Price * tradeAmount;
                _playerTradeController.PlayerInventory[index].Quantity = _playerTradeController.PlayerInventory[index].Quantity + tradeAmount;
                
            }
            else
            {
                StartCoroutine(TradeRejection());
            }
            
            //Subtract cost of quantity multiplied by trade amount from player
        }

        if (sellMode)
        {
            if (planetMarket.TradeGoodsList[index].Price * tradeAmount < planetMarket.PlanetWallet && _playerTradeController.PlayerInventory[index].Quantity >= tradeAmount)
            {
                _playerTradeController.traded = true;
                planetMarket.TradeGoodsList[index].Quantity = planetMarket.TradeGoodsList[index].Quantity + tradeAmount; 
                _playerTradeController.floatWallet = _playerTradeController.floatWallet + ((float)planetMarket.TradeGoodsList[index].Price * tradeAmount);
                //_playerTradeController.activeStorage = _playerTradeController.activeStorage + tradeAmount; 
                planetMarket.PlanetWallet = planetMarket.PlanetWallet - (float)planetMarket.TradeGoodsList[index].Price * tradeAmount;
                _playerTradeController.PlayerInventory[index].Quantity =
                    _playerTradeController.PlayerInventory[index].Quantity - tradeAmount; 
                
            }
            else
            {
                StartCoroutine(TradeRejection());
            }
            
            //Add cost of quantity multiplied by the trade amount to player
        }
        
        
    }

}
