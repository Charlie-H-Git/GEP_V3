using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public WaypointManager WaypointManager;
    public PlayerTradeController PTC;
    private int a;
    public void TimeGo()
    {
        for (int i = 0; i < PTC.PlayerInventory.Count; i++)
        {
            a = a + PTC.PlayerInventory[i].Quantity;
            PTC.activeStorage = a;
        }
        WaypointManager.allowWaypoint = true;
        //Debug.Log("Time Changed");
        Time.timeScale = 1f;
    }
}
