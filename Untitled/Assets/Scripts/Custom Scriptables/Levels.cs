using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public Level[] AllLevels;

    private void Awake() {
        if (AllLevels.Length != transform.childCount){
            Debug.Log("Levels doesnt match!");
            return;
        }
        // Setup levels
        for (int i = 0; i < AllLevels.Length; i++){
            // Image _levelImage = transform.GetChild(i).GetComponentInChildren(typeof(Image)) as Image;
            // _levelImage.sprite = AllLevels[i].mat;
            transform.GetChild(i).GetComponent<Renderer>().material = AllLevels[i].textureMaterial;
            Hoverable.CreateComponent(transform.GetChild(i).gameObject, AllLevels[i].hoverText);
        }
    }
    
}
