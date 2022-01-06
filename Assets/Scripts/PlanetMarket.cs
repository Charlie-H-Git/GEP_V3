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
            int a = TradeGoodsList[i].Quantity;
            int b = TradeGoodsBaselines[i].Quantity;
            double basePrice = TradeGoodsBaselines[i].Price;
            int dif = b - a;
            
            float multiplier = (float)dif / 10f;
            if (multiplier < 0)
            {
                multiplier = 1;
            }

            TradeGoodsList[i].Price = TradeGoodsBaselines[i].Price * multiplier;
            
            //Debug.Log( $"Item Name: " + TradeGoodsList[i].ItemType + $" multiplier = " + multiplier + $" Base Price = " + basePrice + $" Price Result = " + TradeGoodsList[i].Price );
        }

        yield return new WaitForSeconds(5);
        print("ding");
         StartCoroutine(PriceCalc());

    }

    public int dif;
    private float negMultiplier;
    private float multiplier; 
    private void Awake()
    {
        //Planet List = a
        int a = TradeGoodsList[0].Quantity;
        //Reference List = b
        int b = TradeGoodsBaselines[0].Quantity;
        double basePrice = TradeGoodsBaselines[0].Price;
        double price = TradeGoodsList[0].Price;

        if (TradeGoodsBaselines[0].Quantity < TradeGoodsList[0].Quantity)
        {
            
             dif = a - b;
             negMultiplier = (dif - 5f) / 100f;
             //this.negMultiplier = Mathf.Clamp01(negMultiplier);
             Debug.Log($"Difference = " + dif + $" posMultiplier = " + multiplier + $" negMultiplier = " + negMultiplier);
             TradeGoodsList[0].Price = TradeGoodsBaselines[0].Price - this.negMultiplier;
             if (TradeGoodsList[0].Price < 0)
             {
                 TradeGoodsList[0].Price = 1.5;
             }
             //TradeGoodsList[0].Price = TradeGoodsList[0].Price + 1;
        }
        else
        {
            if (TradeGoodsBaselines[0].Quantity > TradeGoodsList[0].Quantity)
            {
                dif = b - a; 
                multiplier = (dif - 10) / 10f;
                if (multiplier == 0)
                {
                    multiplier = 1;
                }
                Debug.Log($"Difference = " + dif + $" posMultiplier = " + multiplier + $" negMultiplier = " + negMultiplier);
                TradeGoodsList[0].Price = TradeGoodsBaselines[0].Price + (multiplier);
            }
        }
        
       
        
        
        

        

        
        
        //StartCoroutine(PriceCalc());
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
