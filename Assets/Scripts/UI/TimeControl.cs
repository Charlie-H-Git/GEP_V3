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
        WaypointManager.allowWaypoint = true;
        //Debug.Log("Time Changed");
        Time.timeScale = 1f;
    }
}
