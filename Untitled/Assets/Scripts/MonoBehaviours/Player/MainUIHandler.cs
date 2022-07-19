using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    private bool lookingAtCredit;


    private void Update()
    {
        if (lookingAtCredit)
        {
            References.Instance.itemPopup.text = "to close";
            if(Input.GetKeyDown(KeyCode.E))
                CloseCredit();
                return;
        }
        
        if (References.Instance.freezWorld) return;

        if (Input.GetKeyDown(KeyCode.E)){
            CheckLevelClick();
            CheckCreditsClick();
        }
        
    }

    private void CheckLevelClick(){
        Ray _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, 2f)){
            // If its a level
            Level _lvl = hit.transform.GetComponent<Level>();
            if (_lvl){
                if (_lvl.unlocked){
                    SceneManager.LoadScene(_lvl.sceneIndex);
                }
            }
        }
    }

    private void CheckCreditsClick(){
        Ray _ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, 2f)){
            // If its credits
            if (hit.transform.CompareTag("Credits")){
                // Activate credits
                References.Instance.CreditsUI.SetActive(true);
                if (References.Instance.CreditsUI.activeInHierarchy)
                {
                    References.Instance.freezWorld = true;
                    lookingAtCredit = true;
                }
            }
        }
    }

    private void CloseCredit()
    {
        References.Instance.CreditsUI.SetActive(false);
        References.Instance.freezWorld = false;
        lookingAtCredit = false;
    }
}
