using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRay : MonoBehaviour
{
    private Vector3 planPos;

    public float rayDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        planPos = gameObject.transform.position;
        
        Debug.DrawRay(planPos, -transform.up * rayDist,Color.green);
    }
}
