using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private WaypointController _waypointController;
    private WaypointManager _waypointManager;

    public float turnSpeed;

    public bool waypointExists;

    [Header("Movement settings")] 
    public float toVel = 2.5f;

    public float maxVel = 15.0f;

    public float maxForce = 40.0f;

    public float gain = 5f;

    private float angle;
    private Vector3 EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();

       
        if (waypointExists)
        {
            _waypointController = FindObjectOfType<WaypointController>();
        }

        _waypointManager = FindObjectOfType<WaypointManager>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position,Vector3.up * 10,Color.cyan);
    }

    public float turnRate = 0.25f;
    IEnumerator TurnCoroutine()
    {
        Vector3 dist = _waypointManager.currentWaypoint.transform.position - transform.position;
        dist.z = 0;
        //yield return new WaitForSeconds(2);

        var startRotation = transform.rotation;
        var endRotation = Quaternion.LookRotation(Vector3.forward, dist);
        var t = 0f;

        var angle = Quaternion.Angle(startRotation,endRotation);
        
        var estTime = angle/turnRate;

        while (t<estTime)
        {
            t = Mathf.Clamp(t + Time.deltaTime, 0f , estTime);
            rb.MoveRotation(Quaternion.Slerp(startRotation, endRotation, t / estTime));
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.1f);
    }
    // Update is called once per frame
    
    private Coroutine turnCo;
    //[Range(0,10)]public float lerpTime;
    void FixedUpdate()
    {
        
            if (_waypointManager.currentWaypoint != null)
            {
                if (turnCo != null)
                {
                    StopCoroutine(turnCo);
                }
                turnCo = StartCoroutine(TurnCoroutine());
            
            
                Vector3 dist = _waypointManager.currentWaypoint.transform.position - transform.position;
               
                dist.z = 0;
                float mag = dist.sqrMagnitude;
                //Calculate Target velocity proportionally to distance
                Vector3 tgVel = Vector3.ClampMagnitude(toVel * dist, maxVel);
                //Debug.Log("magnitude" + mag);
                //Calculate velocity error
                Vector3 error = tgVel - rb.velocity;

                Vector3 force = Vector3.ClampMagnitude(gain * error, maxForce);
            
                rb.AddForce(force,ForceMode.Acceleration);
                // if (mag < 10)
                // {
                //     rb.velocity = Vector3.Lerp(force,Vector3.zero,  mag);
                // }
            }

            if (_waypointManager.currentWaypoint == null)
            {
                rb.velocity = Vector3.zero;
            }
    }
}
