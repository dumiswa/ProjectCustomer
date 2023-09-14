using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampFireIgnition : MonoBehaviour
{
    public Camera camera;

    public Transform campFire;

    bool isLockedOn = false;

    public float followSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            isLockedOn = !isLockedOn;
        }


        if (isLockedOn && campFire != null)
        {
            Vector3 lockInPosition = campFire.position;
            lockInPosition.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, lockInPosition, followSpeed * Time.deltaTime);
            print("lockIn");
        }
    }
}
