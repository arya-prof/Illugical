using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    private bool lookingAtCredit;

    [SerializeField] private Transform playerBody;
    
    [SerializeField] private float mouseSens = 100f;
    
    private float _xRotation = 0f;

    [SerializeField] private Image crossHair;
    private int clearPopup;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        References.Instance.itemPopup.text = "";
        crossHair.transform.localScale = new Vector3(0f,0f,0f);
    }

    private void Update()
    {
        clearPopup = 0;
        if (lookingAtCredit)
        {
            References.Instance.itemPopup.text = "to close";
            if(Input.GetKeyDown(KeyCode.E))
                CloseCredit();
            return;
        }
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
        if (Physics.Raycast(_ray, out hit, 2f, References.Instance.itemLayer))
        {
            crossHair.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            ILevel iLevel = hit.transform.gameObject.GetComponent<ILevel>();
            if (iLevel != null)
            {
                if (iLevel.levelLock)
                {
                    References.Instance.itemPopupE.SetActive(false);
                    References.Instance.itemPopup.text = "Locked";
                }
                else
                {
                    References.Instance.itemPopupE.SetActive(true);
                    References.Instance.itemPopup.text = "to Play";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        iLevel.StartLevel();
                    }
                }
            }

            Hoverable credit = hit.transform.gameObject.GetComponent<Hoverable>();
            if (credit != null)
            {
                References.Instance.itemPopup.text = credit.hoverText;
                References.Instance.itemPopupE.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    References.Instance.CreditsUI.SetActive(true);
                    References.Instance.freezWorld = true;
                    lookingAtCredit = true;
                    References.Instance.itemPopup.text = "to close";
                }
            }
        }
        else
        {
            // Every object that has Item layer must have ItemHandler
            References.Instance.itemPopup.text = "";
            References.Instance.itemPopupE.SetActive(false);
            clearPopup++;
        }
        
        if (clearPopup >= 2){
            References.Instance.itemPopup.text = "";
            crossHair.transform.localScale = new Vector3(1,1,1);
        }
    }

    private void CloseCredit()
    {
        References.Instance.CreditsUI.SetActive(false);
        References.Instance.freezWorld = false;
        lookingAtCredit = false;
    }
}
