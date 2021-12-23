using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlanetMarket : MonoBehaviour
{
    public int _tradeMode = 0;

    private bool _playerPrescence;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print(_tradeMode);
            _tradeMode++;
            if (_tradeMode >= 3)
            {
                _tradeMode = 0;
            }
            
        }
        switch (_tradeMode)
        {
            case 2 :
                BuyMode();
                break;
            case 1 :
                SellMode();
                break;
            case 0:
                NeutralMode();
                break;
        }
    }

    void BuyMode()
    {
        //Debug.Log("BuyMode");
    }
    private void SellMode()
    {
        //Debug.Log("SellMode");
    }

    private void NeutralMode()
    {
        //Debug.Log("NeutralMode");
    }


}
