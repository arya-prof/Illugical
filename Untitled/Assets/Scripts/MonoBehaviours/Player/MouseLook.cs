using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    
    [SerializeField] private float mouseSens = 100f;
    
    private float _xRotation = 0f;

    [SerializeField] private Image crossHair;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!HandController.handController.HandControlleAble) return;
        // Item Pickup
        Ray _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        // Checks raycast
        if (Physics.Raycast(_ray, out hit, 2f, References.Instance.itemLayer)){
            crossHair.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            ItemHandler _itemHandler = hit.transform.gameObject.GetComponent<ItemHandler>();
            // If its an item
            if (_itemHandler){ // Just to make sure it doesn't raise any error
                // Show popup
                References.Instance.itemPopup.text = "Press E to pickup";
                if (Input.GetKeyDown(KeyCode.E)){
                    _itemHandler.Pickup();
                }
            }
        }
        else { // Every object that has Item layer must have ItemHandler
            References.Instance.itemPopup.text = "";
            crossHair.transform.localScale = new Vector3(1,1,1);
        }
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
