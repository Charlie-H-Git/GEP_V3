using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;


public class PlanetMarket : MonoBehaviour
{
    public List<TradeGoods> TradeGoodsList;
    public List<TradeGoodsBaseline> TradeGoodsBaselines;

    /// <summary>
    /// price and quantity as baselines
    ///
    /// every 60 seconds unless trading, cycle through the quantities
    /// and if they are below their baselines then the price is MULTIPLIED dependant on a
    /// factor of how below their quantity and vicer-versa for above quantity
    /// the new updated values are reflected in the TradeGoods public Class List
    /// </summary>

    IEnumerator PriceCalc()
    {
        for (int i = 0; i < TradeGoodsList.Count; i++)
        {
            //Planet List = a
            int a = TradeGoodsList[i].Quantity;
            //Reference List = b
            int b = TradeGoodsBaselines[i].Quantity;
            double basePrice = TradeGoodsBaselines[i].Price;
            double price = TradeGoodsList[i].Price;

            if (TradeGoodsBaselines[i].Quantity < TradeGoodsList[i].Quantity)
            {
            
                dif = a - b;
                negMultiplier = (dif - 5f) / 100f;
                //this.negMultiplier = Mathf.Clamp01(negMultiplier);
                //Debug.Log($"Difference = " + dif + $" posMultiplier = " + multiplier + $" negMultiplier = " + negMultiplier);
                TradeGoodsList[i].Price = TradeGoodsBaselines[i].Price - this.negMultiplier;
                if (TradeGoodsList[i].Price < 0)
                {
                    TradeGoodsList[i].Price = 1.5;
                }
             
            }
            else
            {
                if (TradeGoodsBaselines[i].Quantity > TradeGoodsList[i].Quantity)
                {
                    dif = b - a; 
                    multiplier = (dif - 10) / 15f;
                    if (multiplier == 0)
                    {
                        multiplier = 1;
                    }
                    //Debug.Log($"Difference = " + dif + $" posMultiplier = " + multiplier + $" negMultiplier = " + negMultiplier);
                    TradeGoodsList[i].Price = TradeGoodsBaselines[i].Price + (multiplier);
                }
            }
       
        }

        yield return new WaitForSeconds(0.5f);
        //print("ding");
        StartCoroutine(PriceCalc());

    }

    public int dif;
    private float negMultiplier;
    private float multiplier; 
    private void Awake()
    {
        StartCoroutine(PriceCalc());
    }
}
[System.Serializable]
public class TradeGoods
{
    public ItemType ItemType;
    public double Price;
    public int Quantity;
}
[System.Serializable]
public class TradeGoodsBaseline
{
    public ItemType ItemType;
    public double Price;
    public int Quantity;
}
