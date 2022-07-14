using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    
    [SerializeField] private float mouseSens = 100f;
    
    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        // Item Pickup
        Ray _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        // Checks raycast
        if (Physics.Raycast(_ray, out hit, 5f, References.Instance.itemLayer)){
            ItemHandler _itemHandler = hit.transform.gameObject.GetComponent<ItemHandler>();
            // If its an item
            if (_itemHandler){ // Just to make sure it doesn't raise any error
                // Show popup
                References.Instance.itemPopup.text = "E";
                if (Input.GetKeyDown(KeyCode.E)){
                    _itemHandler.Pickup();
                }
            }
        }
        else { // Every object that has Item layer must have ItemHandler
            References.Instance.itemPopup.text = "";
        }
    }
}
