using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
    public Camera rayCamera;

    public float rayDistance;

    bool isItemSelected = false;

    public GameObject selectedItem;
    public GameObject lastSelectedItem;
    public GameObject pickedUpItem;

    public Transform environment;

    public Transform pickUpPoint;
    public Transform fireBlueprint;

    public Color originalColor;
    public Color highlightedColor = Color.cyan;

    LayerMask item;

    RaycastHit hit;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        item = LayerMask.NameToLayer("item");

        
    }


    void Update()
    {

        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.tag == "Item")
        {
            selectedItem = hit.collider.gameObject;

            if (hit.transform.gameObject.layer == item)
            {
                if (isItemSelected == false)
                {
                    isItemSelected = true;

                    originalColor = selectedItem.GetComponent<Renderer>().material.color;
                    selectedItem.GetComponent<Renderer>().material.color = highlightedColor;
                    lastSelectedItem = selectedItem;
                }

                if (selectedItem != lastSelectedItem && isItemSelected)
                {
                    if (lastSelectedItem != null)
                    {
                        lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
                    }
                    isItemSelected = false;
                    lastSelectedItem = null;
                }
            }
            else
            {
                if (lastSelectedItem != null)
                {
                    lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
                }
                isItemSelected = false;
                lastSelectedItem = null;
            }
        }
        else
        {
            if (lastSelectedItem != null)
            {
                lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
            }
            isItemSelected = false;
            lastSelectedItem = null;
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.G)) 
        {
            Drop();
        }*/

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedItem != null && pickedUpItem == null)
            {
                PickUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (pickedUpItem != null && selectedItem != lastSelectedItem)
            {
                Drop();
            }
        }

        /*fireBlueprint.position = new Vector3(hit.point.x, 0, hit.point.z);
        fireBlueprint.rotation = Quaternion.identity;*/
    }

    void FixedUpdate()
    {
        
    }

    /*void PickUp()
    {
        *//* if (selectedItem != null && pickedUpItem == null)
         {
             Rigidbody itemRigidBody = selectedItem.GetComponent<Rigidbody>();
             if (itemRigidBody != null)
             {
                 itemRigidBody.useGravity = false;
                 itemRigidBody.isKinematic = true;
             }

             selectedItem.transform.position = pickUpPoint.position;
             selectedItem.transform.rotation = pickUpPoint.rotation;

             selectedItem.transform.SetParent(pickUpPoint);

             pickedUpItem = selectedItem;
             selectedItem = null;
         }*//*

        selectedItem.GetComponent<Rigidbody>().useGravity = false;
        selectedItem.GetComponent<Rigidbody>().isKinematic = true;
        selectedItem.transform.position = Vector3.zero;
        selectedItem.transform.rotation = Quaternion.identity;
        selectedItem.transform.SetParent(pickUpPoint, false);

        pickedUpItem = selectedItem;
    }
    
        void Drop()
        {
            if (pickedUpItem != null)
            {
                pickedUpItem.GetComponent<Rigidbody>().useGravity = true;
                pickedUpItem.GetComponent<Rigidbody>().isKinematic = false;
                pickedUpItem.transform.SetParent(environment);
                pickedUpItem.transform.parent = null;
                pickedUpItem = null;
            }
        }*/

    void PickUp()
    {
        selectedItem.GetComponent<Rigidbody>().useGravity = false;
        selectedItem.GetComponent<Rigidbody>().isKinematic = true;
        selectedItem.transform.position = Vector3.zero;
        selectedItem.transform.rotation = Quaternion.identity;
        selectedItem.transform.SetParent(pickUpPoint, false);

        pickedUpItem = selectedItem;
    }

    void Drop()
    {
        pickedUpItem.GetComponent<Rigidbody>().useGravity = true;
        pickedUpItem.GetComponent<Rigidbody>().isKinematic = false;
        pickedUpItem.transform.parent = null;
        pickedUpItem = null;
    }

}
