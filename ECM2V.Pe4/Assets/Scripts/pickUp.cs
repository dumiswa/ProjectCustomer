using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
    public Camera rayCamera;

    public float rayDistance =3f;
    public float distance;

    bool isItemSelected = false;

    public GameObject selectedItem;
    public GameObject lastSelectedItem;
    public GameObject pickedUpItem;

    public Transform pickUpPoint;

    public Color originalColor;
    public Color highlightedColor = Color.cyan;

    LayerMask logs;
    LayerMask rocks;

    RaycastHit hit;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        logs = LayerMask.NameToLayer("Log");
        rocks = LayerMask.NameToLayer("Rock");
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        }
    }

    void FixedUpdate()
    {
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.tag == "Item")
        {
            selectedItem = hit.collider.gameObject;

            if (hit.transform.gameObject.layer == logs)
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
    }

    void PickUp()
    {
        if (selectedItem != null)
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
        }
    }
    

        void Drop()
        {
        pickedUpItem.GetComponent<Rigidbody>().useGravity = true;
        pickedUpItem.GetComponent<Rigidbody>().isKinematic = false;
        pickedUpItem.transform.parent = null;
        pickedUpItem = null;
    }
    
}
