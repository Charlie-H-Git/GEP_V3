using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public UnityEngine.GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerVector = new Vector3(Player.transform.position.x, Player.transform.position.y,-15);
        transform.position = playerVector;

    }

}
