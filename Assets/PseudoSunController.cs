using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoSunController : MonoBehaviour
{
    private Vector3 dif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 posMatch = Vector2.zero;
        posMatch.z = -2.09f;

        gameObject.transform.localPosition = posMatch;
        Debug.DrawRay(gameObject.transform.position,posMatch);
    }
}
