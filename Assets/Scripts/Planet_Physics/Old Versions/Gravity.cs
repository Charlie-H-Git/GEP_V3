using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float f; //force
    
    private const float PhysicsMultiplier = 5000f;
    [SerializeField]private float G = 6.674e-11f; //Gravitational Constant
    
    private Rigidbody m1;//Mass 1 The Sun ( this Game Object)
    private Rigidbody m2;//Mass 2 Planet
    private Vector3 r1;
   
   // public Dictionary<string, Rigidbody> planetDict = new Dictionary<string, Rigidbody>();
   private List<Rigidbody> planetRigidBody = new List<Rigidbody>();
    public Vector3 FORCE;

    public GameObject[] planets;
    
    public bool debugBool;
    void Start()
    {
        m1 = GetComponent<Rigidbody>();
        m1.mass = 1.989e+30f * 1e-30f;
        planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (var g in planets)
        {
            planetRigidBody.Add(g.GetComponent<Rigidbody>());
        }

        StartCoroutine("planetMove");
    }

    private void DebugClass()
    {
        if (debugBool)
        {
            print(m1.mass);
            Debug.Log(m2.velocity); 
            Debug.Log("TRIGGERED");
            Debug.Log($"r1 {r1.normalized} FORCE {FORCE}");
        }
    }

    private void Update()
    {
        DebugClass();
    }

    //private int i = 0;
    private void OnTriggerStay(Collider other)
    {
        // Rigidbody rb;
        // if (planetDict.TryGetValue(other.name, out rb))
        // {
        //
        // }
        // else
        // {
        //     planetDict.Add(other.name, other.gameObject.GetComponent<Rigidbody>());
        // }

    }
    void HandlePlanet(Rigidbody m2){
        
            //assigns other objects rigidbody to m2
            Transform other = m2.transform;
            // get sun position
            Vector3 Sun = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            // planet
            Vector3 otherPlanet = new Vector3(other.position.x, other.position.y, other.position.z);
      
       
            //Difference between each object
            Vector3 r1 = Sun - otherPlanet;
     
            //Difference between each object squared
            float r2 = r1.sqrMagnitude;
            float distance = r1.magnitude;
        
            
            // Calculates Force by Mutliplying Gravitational
            // constant by the mass of each object divided 
            // by the distance between each object squared
            
            f = G * m1.mass * m2.mass * PhysicsMultiplier  / r2;
        
        
            //Assigns the value r1 normalized between 0 and 1
            //multiplied by F independent of frames per second
            Vector3 FORCE = r1.normalized * (f * Time.deltaTime);
        
            // Adds force to the other game objects rigidbody using the FORCE vector
            // Using the Force force mode
            m2.AddForce(FORCE);
        
        
            
        

    }

    IEnumerator planetMove()
    {
        foreach (var p in planetRigidBody)
        {
            HandlePlanet(p);
            yield return new WaitForSeconds(.5f);
        }
    }
}
