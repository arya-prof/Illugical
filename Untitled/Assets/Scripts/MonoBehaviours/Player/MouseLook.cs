using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    
    [SerializeField] private float mouseSens = 100f;
    
    private float _xRotation = 0f;

    [SerializeField] private Image crossHair;
    private int clearPopup;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        clearPopup = 0;
        if (References.Instance.freezWorld) return;
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
        //
        
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
            
            IDoor door = hit.transform.gameObject.GetComponent<IDoor>();
            if (door != null)
            {
                if (!door.doorOpened)
                {
                    if (door.itemLock)
                    {
                        if (door.itemContaine)
                        {
                            References.Instance.itemPopup.text = "Press E to unlock";
                            if (Input.GetKeyDown(KeyCode.E)){
                                door.OnIntract();
                            }
                        }
                        else
                        {
                            References.Instance.itemPopup.text = "Locked";
                        }
                    }
                    else
                    {
                        References.Instance.itemPopup.text = "Press E to open";
                        if (Input.GetKeyDown(KeyCode.E)){
                            door.OnIntract();
                        }
                    }
                }
                else
                {
                    References.Instance.itemPopup.text = "";
                }
            }
            
            IIntract intract = hit.transform.gameObject.GetComponent<IIntract>();
            if (intract != null)
            {
                if (intract.open)
                {
                    References.Instance.itemPopup.text = "Press E to use";
                    if (Input.GetKeyDown(KeyCode.E)){
                        intract.OnActivate();
                    }
                }
                else
                {
                    References.Instance.itemPopup.text = "Unable to use";
                }
            }
        }
        else { // Every object that has Item layer must have ItemHandler
            clearPopup++;
        }


        if (Physics.Raycast(_ray, out hit, 2f)){
            
            Hoverable _hoverable = hit.transform.gameObject.GetComponent<Hoverable>();
            // If its a Hoverable
            if (_hoverable){
                // Show popup
                crossHair.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
                References.Instance.itemPopup.text = _hoverable.hoverText;
            }
            else {
                clearPopup++;
            }
        }
        else {
            clearPopup++;
        }

        if (clearPopup >= 2){
            References.Instance.itemPopup.text = "";
            crossHair.transform.localScale = new Vector3(1,1,1);
        }
    }
}
