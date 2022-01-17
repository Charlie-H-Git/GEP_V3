using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletUpdate : MonoBehaviour
{
    public PlayerTradeController PTC;
    public PlanetMarket _planetMarket;
    private TMP_Text text;
    [Header("Planets")]
    public GameObject[] planetGameObjectArray;
    
    private void OnEnable()
    {
        foreach (var p in planetGameObjectArray)
        {
            if (p.GetComponent<PlanetMarket>().trading)
            {
                _planetMarket = p.GetComponent<PlanetMarket>();
                //Debug.Log("planet market assigned");
                text = gameObject.GetComponent<TMP_Text>();
            }
        }
        StartCoroutine(walletUiUpdate());
    }

    IEnumerator walletUiUpdate()
    {
        if (_planetMarket.trading)
        {
            text.text = PTC.floatWallet.ToString("C");
            yield return new WaitForSeconds(0.1f);
            walletUiUpdate(); 
        }
    }
}
