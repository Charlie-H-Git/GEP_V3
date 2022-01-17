using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointController : MonoBehaviour
{
    private PlayerController _playerController;
    private WaypointManager _waypointManager;

    public bool arrived = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _waypointManager = FindObjectOfType<WaypointManager>();
    }

    private void OnDestroy()
    {
        _waypointManager.waypoints.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived && gameObject != _waypointManager.currentWaypoint)
        {
            Destroy(gameObject, 1);
        }

        Vector3 wayPos = gameObject.transform.position;
        RaycastHit hit;
        Debug.DrawRay(wayPos,Vector3.forward *10);
        if (Physics.Raycast(wayPos, Vector3.forward,out hit,10,LayerMask.GetMask("RaycastPlanet")))
        {
            //if (hit.transform.gameObject.CompareTag("Planet"))
            {
                //Debug.Log("PLANET");
                gameObject.transform.parent = hit.collider.gameObject.transform;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("TRIGGERED");
        if (other.gameObject.CompareTag("Player"))
        {
            arrived = true;
            //Debug.Log("Player has Arrived");
            if (arrived)
            {
                Destroy(this.gameObject);  
            }
        }
    }
    
}
