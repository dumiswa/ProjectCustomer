 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    void Start()
    {
        //assign transform to cameraPos
        cameraPosition = GameObject.Find("CameraPos").transform;   
    }


    void Update()
    {
        // Set the position of this object to cameraPosition
        transform.position = cameraPosition.position;
    }
}
