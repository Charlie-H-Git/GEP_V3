using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private PlayerController _playerController;
    public List<UnityEngine.GameObject> waypoints = new List<UnityEngine.GameObject>();
    public GameObject tradeHud;
    private Vector3 _worldPos;
    
    
    private UnityEngine.GameObject _currentWaypoint;

    public UnityEngine.GameObject waypointGO;

    public int waypointCount;

    private void Start()
    {
        allowWaypoint = true;
    }

    public UnityEngine.GameObject currentWaypoint
    {
        get
        {
            if (waypoints.Count > 0)
            {
                _currentWaypoint = waypoints[0];
                return _currentWaypoint;
            }
            else
            {
                return null;
            }
        }
    }

    public bool allowWaypoint;
    void Waypoint()
    {
        
        waypoints.Capacity++;

        if (waypointCount + 1 < waypoints.Count)
        {
            waypointCount++;
        }

        _worldPos.z = -10;
        UnityEngine.GameObject waypointCr = Instantiate(waypointGO, _worldPos, Quaternion.identity, transform);
        waypointCr.transform.parent = gameObject.transform;
        waypoints.Add(waypointCr);

        if (waypoints.Count >= 1)
        {
            for(int i = 1; i < waypoints.Count; i ++)
            {
                waypoints.RemoveAt(0);
            }
        }
    }

    private void Update()
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        _worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && allowWaypoint ) 
        {
            Waypoint();
        }
        
    }
}
