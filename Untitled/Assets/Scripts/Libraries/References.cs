using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    public static References Instance { get; private set; }

    // Put your global values in here
    public Transform cameraTransform;
    public List<Item> playerBackpack = new List<Item>();
    public LayerMask itemLayer;

    private void Awake() {
        Instance = this;
    }
}
