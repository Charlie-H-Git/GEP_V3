using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
[System.Serializable]
public class Planet
    {
    public string Name;
    public GameObject _planet;
    public Rigidbody _Rigidbody;
    public float _realMass;
    public float _CalculatedMass;
    public float IndvVelocity;
    public float IndvForce;
    }

public class PhysicsController : MonoBehaviour
{
    [Header("Planet Physics Settings")]
    public float v;
    private const float massScale = 1e-24f;
    private float[] _Planetmass = {5.972e+24f,4.867e+24f,1.898e+25f};
    private Vector3 motion;
    public List<Planet> planets;
    [Header("Sun Physics")]
    public float f; //force
    private const float PhysicsMultiplier = 1000f;
    [SerializeField]private float G = 6.674e-11f; //Gravitational Constant
    private Rigidbody m1;//Mass 1 The Sun ( this Game Object)
    public Rigidbody m2;//Mass 2 Planet
    private Vector3 r1;
    public GameObject[] planetGameObjectArray;
    
    /// <summary>
    ///                     [AWAKE CLASS DESCRIPTION]
    /// This awake function assigns the Suns rigidbody to the component field m1.
    /// Converts M1's mass to the suns real mass to a decimal point of itself.
    /// Changes the Time Scale to 10x RealTime
    /// Adds every game object with the tag "Planet" to the planetGameObjectArray
    ///
    /// Then for each element in the planetGameObjectArray Add a new Planet(); 
    /// and assign each value to each field in the class List element for each planet in the gameObject array
    /// Then add the Planet(); to the respective element to the list planets.
    ///
    /// Start the coroutine to handle each planets movement
    /// Start the coroutine to handle each planets rotation.
    /// </summary>
    void Awake()
    {
        m1 = GetComponent<Rigidbody>();
        m1.mass = 1.989e+30f * 1e-30f;
        Time.timeScale = 1f;
        planetGameObjectArray = GameObject.FindGameObjectsWithTag("Planet");
        
        
        //For each GameObject in planetGameObjectArray add a new planet class element to the planet list 
        for (int i = 0; i < planetGameObjectArray.Length; i++)
        {
            var p = new Planet
            {
                Name = planetGameObjectArray[i].name,
                _planet = planetGameObjectArray[i],
                _Rigidbody = planetGameObjectArray[i].GetComponent<Rigidbody>(),
                _realMass = _Planetmass[i],
                _CalculatedMass = _Planetmass[i] * massScale
            };
            p._Rigidbody.mass = p._CalculatedMass;
            planets.Add(p);
        }
        
        StartCoroutine(planetMove(planets));
        StartCoroutine(PlanetRotate(planets));
    }
    
    /// <summary>
    /// Take the parsed in _gameObject from the handle rotation coroutine and rotate its transform to look at the Vector3.zero (Suns Position)
    /// </summary>
    void ObjectLook(GameObject _gameObject)
    {
        _gameObject.transform.LookAt(Vector3.zero);
    }

    
    void HandlePlanet(Rigidbody m2, List<Planet> planets, GameObject _gameObject){
        
        //Assigns the Transform value other M2 Rigidbody transform
        Transform other = _gameObject.transform;
        // get sun position
        Vector3 Sun = gameObject.transform.position;
        // planet
        Vector3 otherPlanet = new Vector3(other.position.x, other.position.y, other.position.z);
        //Difference between each object
        r1 = Sun - otherPlanet;
        //Difference between each object squared
        float r2 = r1.sqrMagnitude;
        float distance = r1.magnitude;
        // Calculates Force by Mutliplying Gravitational
        // constant by the mass of each object divided 
        // by the distance between each object squared
        f = G * m1.mass * m2.mass * PhysicsMultiplier / r2;
        //Assigns the value r1 normalized between 0 and 1
        //multiplied by F independent of frames per second
        Vector3 FORCE = r1.normalized * (f * Time.deltaTime);
        // Adds force to the other game objects rigidbody using the FORCE vector
        // Using the Force force mode
        //print(m2.gameObject.name);
        m2.AddForce(FORCE);
    }
    // void PlanetRotation(Rigidbody m2)
    // {
    //     Vector3 planetPos = m2.transform.position;
    //     float r = r1.sqrMagnitude;
    //     v = Mathf.Sqrt(f * m2.mass ) * (PhysicsMultiplier)/r;
    //     motion = m2.transform.up * v ;
    //     if (planetPos.x > 0 && planetPos.y > 0)
    //     {
    //         motion.x = Mathf.Abs(motion.x);
    //         motion.y = -Mathf.Abs(motion.y);
    //     }
    //     else if (planetPos.x > 0 && planetPos.y < 0)
    //     {
    //         motion.x = -Mathf.Abs(motion.x);
    //         motion.y = -Mathf.Abs(motion.y);
    //     }
    //     m2.velocity = motion;
    //     //Debug.Log("motion applied");
    // }
    
    
    IEnumerator planetMove(List<Planet> planets)
    {
        for (int i = 0; i < planetGameObjectArray.Length; i++)
        {
            if ( planets[i]._Rigidbody)
            {
                m2 = planets[i]._Rigidbody;
                GameObject _gameObject = planets[i]._planet;
                Debug.Log($"planets[i] = "+ i + m2.name + _gameObject.name);
                HandlePlanet(m2, planets, _gameObject);
                //PlanetRotation(m2);
                planets[i].IndvVelocity = v;
                planets[i].IndvForce = f;
                //Debug.Log("planet Move");
                yield return new WaitForSeconds(.5f);
                if (i >= 2)
                {
                    yield return null; 
                }
            }
            
        }
    }

    IEnumerator PlanetRotate(List<Planet> planets)
    {
        for (int i = 0; i < planetGameObjectArray.Length; i++)
        {
            if (planets[i]._planet)
            {
                GameObject _gameObject = planets[i]._planet;
                //Debug.Log($"planets[i] = "+ i + _gameObject.name);
                ObjectLook(_gameObject);
                yield return new WaitForSeconds(0.5f);
                if (i >= 2)
                {
                    i = -1;
                    
                }
            }
        }
        
    }
}
    
   

