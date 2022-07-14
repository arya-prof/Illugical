using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class References : MonoBehaviour
{
    public static References Instance { get; private set; }

    // Put your global values in here
    public Transform cameraTransform;
    public List<Item> playerBackpack = new List<Item>();
    public LayerMask itemLayer;
    public GameObject itemPopup;

    private void Awake() {
        Instance = this;
    }
}
