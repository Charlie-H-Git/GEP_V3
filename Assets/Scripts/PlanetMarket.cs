using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class PlanetMarket : MonoBehaviour
{
    public List<TradeGoods> TradeGoodsList;
    public Canvas tradeCanvas;

    private void Awake()
    {
        //TradeGoodsList = new List<TradeGoods>();
    }
}
[System.Serializable]
public class TradeGoods
{
    public ItemType ItemType;
    public double Price;
}