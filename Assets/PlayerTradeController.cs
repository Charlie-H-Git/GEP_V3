using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class PlayerTradeController : MonoBehaviour
{
    public float floatWallet;

    public Canvas storageHUD;

    public GameObject unit;
    private List<GameObject> _unitList = new List<GameObject>();

    public List<TradeGoods> PlayerInventory;
    
    public TMP_Text walletTMPText;
    public TMP_Text cargoTMPText;
    
    public int storageCapacity;
    [Range(0,500)]public int activeStorage;
    private float _unitAmount;
    private int unitCount = 66;

    private void Start()
    { 
        for (int i = 0; i < unitCount; i++)
        {
            if (storageHUD.transform.childCount < unitCount)
            {
               GameObject newGO = Instantiate(unit, storageHUD.transform);
               _unitList.Add(newGO);
               _unitList[i].gameObject.SetActive(false);
            }
        }
        //Debug.Log(_unitList.Count);
        StartCoroutine(InventoryTick());
    }

    private int _previousActiveUnits;
    IEnumerator InventoryTick()
    {
        Wallet();
        cargo();
        //Debug.Log($"activeUnits = " + activeUnits + $" previousActiveUnits = " + _previousActiveUnits +  $" nullUnits = " + nullUnits);
        yield return new WaitForSeconds(0.1f);
        
        StartCoroutine(InventoryTick());
    }

    private void cargo()
    {
        
        int division = storageCapacity / unitCount;
        int activeUnits = storageCapacity - (storageCapacity - activeStorage);
        activeUnits = activeUnits / division;
        //Debug.Log(division);
        foreach (var gameObject in _unitList)
        {
            int index = gameObject.transform.GetSiblingIndex();
            if (gameObject.activeInHierarchy && index > activeUnits)
            {
                gameObject.SetActive(false);
            }else if (gameObject.activeInHierarchy == false && index < activeUnits)
            {
                gameObject.SetActive(true);
            }
        }

        if (activeStorage == 0)
        {
            storageHUD.transform.GetChild(0).gameObject.SetActive(false);
        }

        cargoTMPText.text = activeStorage + "/" + storageCapacity;

    }

    public void Wallet()
    {
        walletTMPText.text = floatWallet.ToString("C");
        
    }
    
    private void Update()
    {
        
        
        
    }
}
