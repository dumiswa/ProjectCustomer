using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class raycast : MonoBehaviour
{
    public float maxDistance = 10f;
    public GameObject selectedItem;
    public GameObject lastSelectedItem;

    public GameObject pickedUpItem;
    public Transform pickUpPoint;

    public Transform fireBlueprint;

    bool isItemSelected = false;

    LayerMask itemLayerMask;
    LayerMask groundLayerMask;

    RaycastHit hitInfo;

    public Color originalColor;
    public Color highlightedColor = Color.yellow;



     void Start()
    {
        itemLayerMask = LayerMask.NameToLayer("Item");
        groundLayerMask = LayerMask.NameToLayer("whatIsGround");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {

            if (selectedItem != null && pickedUpItem == null)
            {
                PickUp();
            }

        }

        if (Input.GetKeyDown(KeyCode.G))
        {

            if (pickedUpItem != null /*&& selectedItem != lastSelectedItem*/)
            {
                Drop();
            }

        }


    }

    void FixedUpdate()
    {
        Ray rayCast = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0f));
        
        //bool raycastHit = Physics.Raycast(rayCast, out hitInfo, maxDistance, itemLayerMask);
        bool raycastHit = Physics.Raycast(rayCast, out hitInfo, maxDistance);

       // fireBlueprint.position = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
       //fireBlueprint.localPosition = new Vector3(fireBlueprint.position.x, -0.5f, fireBlueprint.position.z);
       // fireBlueprint.rotation = Quaternion.identity;

        if (raycastHit)
        {
            if (hitInfo.transform.gameObject.layer == itemLayerMask)
            {
                selectedItem = hitInfo.collider.gameObject;

                if (isItemSelected == false)
                {
                    isItemSelected = true;

                    originalColor = selectedItem.GetComponent<Renderer>().material.color;
                    selectedItem.GetComponent<Renderer>().material.color = highlightedColor;
                    lastSelectedItem = selectedItem;
                }

                if (selectedItem != lastSelectedItem && isItemSelected)
                {
                    isItemSelected = false;
                    lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
                }
            }

            else if (hitInfo.transform.gameObject.layer == groundLayerMask)
            {
                //fireBlueprint.position = hitInfo.point;

                //fireBlueprint.position = new Vector3(0, transform.position.y - 0.5f, transform.position.z + 3);
            }

        }

        else if (!raycastHit && selectedItem != null)
        {
            selectedItem.GetComponent<Renderer>().material.color = originalColor;
            isItemSelected = false;
            selectedItem = null;
        }



    }

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
