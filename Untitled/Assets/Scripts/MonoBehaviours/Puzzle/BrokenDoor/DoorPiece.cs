using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPiece : MonoBehaviour
{
    public Item _item;
    
    private void Awake() {
        this.gameObject.SetActive(false);
    }
}
