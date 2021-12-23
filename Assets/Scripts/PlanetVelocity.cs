using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 
    PlanetVelocity : MonoBehaviour
{
    private List<UnityEngine.GameObject> Planets = new List<UnityEngine.GameObject>();
    private PhysicsControllerV2 _gravity;
    public float v;
    public bool debugBool;
    public GameObject Sun;
    private Vector3 sunPos;
    private Rigidbody rb;
    private Vector3 dif;
    private Vector3 motion;
    public float mass = 0f;
    private const float PhysicsMultiplier = 5000f;
    private const float massScale = 1e-24f;
    
    private void Awake()
    {
        sunPos = Sun.transform.position;
        Time.timeScale = 1f;
        Planets.Add(this.gameObject);
        foreach (UnityEngine.GameObject g in Planets)
        {
            g.gameObject.GetComponent<Rigidbody>().mass = mass * massScale;
        }
    }
    
    void Start()
    {
        Time.timeScale = 50f;
        _gravity = FindObjectOfType<PhysicsControllerV2>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine("UpdatePR");
    }

    void DebugClass()
    {
        if(debugBool)
        {
            
            foreach (UnityEngine.GameObject p in Planets)
            {
                Debug.Log($"Planet Name {p.gameObject.name} Planet Mass {rb.mass}");
            }
            //Debug.DrawRay(gameObject.transform.position, motion, Color.red);
            //Debug.Log($"Calculated Velocity = {v} Difference {dif} Motion {motion}");
        }
    }
    void ObjectLook()
    {
        transform.LookAt(Sun.transform);
    }

    // void PlanetRotation()
    // {
    //    // if(_gravity == null) return;
    //   //  Vector3 sunPos = new Vector3(Sun.transform.position.x, SunTransform.position.y, SunTransform.position.z);
    //   Vector3 planetPos = transform.position;
    //
    //     dif = sunPos - planetPos;
    //     float r = dif.sqrMagnitude;
    //
    //     v = Mathf.Sqrt(_gravity.f * rb.mass )* PhysicsMultiplier / r;
    //     //v = Mathf.Sqrt(_gravity.G * rb.mass / r ) ;
    //
    //     motion = transform.up * v;
    //     if (planetPos.x > 0 && planetPos.y > 0)
    //     {
    //         motion.x = Math.Abs(motion.x);
    //         motion.y = -Math.Abs(motion.y);
    //     }
    //
    //    else if (planetPos.x > 0 && planetPos.y < 0)
    //     {
    //         motion.x = -Math.Abs(motion.x);
    //         motion.y = -Math.Abs(motion.y);
    //     }
    //
    //  //   rb.velocity = motion;
    // }

    //private int i = 0;

    IEnumerator UpdatePR()
    {
        for (;;)
        {
            Debug.Log($"Planet rotate");
            //PlanetRotation();
            yield return new WaitForSecondsRealtime(10f);
            //Time.
        }
          
    }

    void FixedUpdate()
    {
    
   
       
    
        DebugClass();
        ObjectLook();
        
        
    }
}
