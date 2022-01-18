using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PhysicsControllerV2 : MonoBehaviour
{
    private GameObject[] planetGameObjectArray;
    private readonly float[] _planetMass = {1.898e+25f,4.867e+24f,5.972e+24f};
    private readonly float[] _planetDistance = {7.786e+08f,2.2794e+08f,5.489e+08f};
    private const float massScale = 1e-24f;
    public List<_Planet> planets;
    //private int planetListIdentifier;
    private Rigidbody m1;
    private const float DistanceScale = 1e-7f;
    public float distanceUnits = 148900000f;
    public float r;

    private GameObject Sun;
    private void Awake()
    {
        Sun = this.gameObject;
        m1 = GetComponent<Rigidbody>();
        m1.mass = 1.989e+30f * 1e-30f;
        Time.timeScale = 1f;
        planetGameObjectArray = GameObject.FindGameObjectsWithTag("Planet");
        for (int i = 0; i < planetGameObjectArray.Length; i++)
        {
            var p = new _Planet
            {
                _trailRenderer = planetGameObjectArray[i].GetComponent<TrailRenderer>(),
                Name = planetGameObjectArray[i].name,
                _planet = planetGameObjectArray[i],
                _Rigidbody = planetGameObjectArray[i].GetComponent<Rigidbody>(),
                _realMass = _planetMass[i],
                _realDistance = _planetDistance[i],
                _CalculatedMass = _planetMass[i] * massScale,
                sunRigidbody = gameObject.GetComponent<Rigidbody>()
            };
            p._Rigidbody.mass = p._CalculatedMass;
            StartPlanet(p);
            planets.Add(p);
        }
       
        StartCoroutine(PlanetGrav(planets));
       // Start();
    }
    public void StartPlanet(_Planet _planetManager)
    {
        
        _planetManager._planet.transform.position = Vector3.zero + (Vector3.zero - _planetManager._planet.transform.position ).normalized * _planetManager._realDistance * DistanceScale;
        _planetManager._trailRenderer.enabled = true;
    }

    IEnumerator PlanetGrav(List<_Planet> planets)
    {
        for(int _planetListIdentifier = 0; _planetListIdentifier < planets.Count; _planetListIdentifier++)
        {
            //Debug.Log("loop");
            _Planet _planet = planets[_planetListIdentifier];
            _planet.PlanetUpdate();
            _planet.PlanetRotate();
            yield return new WaitForSeconds(0.2f);
            if (_planetListIdentifier >= 2)
            {
                _planetListIdentifier = -1; 
            }
           
        }
    }
}
[Serializable]
public class _Planet
{
    public TrailRenderer _trailRenderer;
    public string Name;
    public GameObject _planet;
    public Rigidbody _Rigidbody;
    public Rigidbody sunRigidbody;
    public float _realMass;
    public float _realDistance;
    public float _CalculatedMass;
    public float IndvVelocity;
    public float IndvForce;
    public float G = 6.674f;
    public float f;
    public float PhysicsMultiplier = 10f;
    public Vector3 distance;

    public void PlanetUpdate()
    {
        //Debug.Log("Rotating");
        //Assigns the transform of current planet gameObject in coroutine loop
        Transform other = _planet.transform;
        //print(planets[planetListIdentifier]._planet.name);
        //Gets the transform Vector of each object and assigns
        //the distance between them to a float 
        //distance = Vector3.Distance(Sun.position,other.position);
        Vector3 SunPos = Vector3.zero;
        Vector3 otherPos = new Vector3(other.position.x, other.position.y, other.position.z);
        distance = SunPos - otherPos;
        float r = distance.magnitude;
        Rigidbody m2 = _Rigidbody;
        //print(m2.name + m2.mass);
        f = G * sunRigidbody.mass * m2.mass * PhysicsMultiplier / (r*r);
        
        Vector3 FORCE = distance.normalized * (f * Time.deltaTime);
        //print(FORCE);
        m2.AddForce(FORCE);
    }

    public void PlanetRotate()
    {
        Transform other = _planet.transform;
        other.LookAt(Vector3.zero,Vector3.up);
        float r = distance.sqrMagnitude;
        float v = Mathf.Sqrt(f * sunRigidbody.mass * PhysicsMultiplier / r);
        IndvVelocity = v;
        Vector3 motion = other.transform.up * v;
        Rigidbody rb = _Rigidbody;
        
        if (other.position.x > 0 && other.position.y > 0)
        {
            motion.x = Math.Abs(motion.x);
            motion.y = -Math.Abs(motion.y);
        }
        else if (other.position.x > 0 && other.position.y < 0)
        {
            motion.x = -Math.Abs(motion.x);
            motion.y = -Math.Abs(motion.y);
        }
        rb.velocity = motion;
    }
}