using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class References : MonoBehaviour
{
    public static References Instance { get; private set; }

    // Player
    public Transform cameraTransform;
    public GameObject otherCanvas;
    public List<Item> playerBackpack = new List<Item>();
    public List<GameObject> playerBackpackUI = new List<GameObject>();
    public CharacterController playerController;
    public LayerMask itemLayer;
    private bool _freezWorld;
    public bool freezWorld
    {
        get
        {
            return _freezWorld;
        }
        set
        {
            _freezWorld = value;
        }
    }

    // UI
    public TMP_Text itemPopup;
    public GameObject inventoryPanel;
    public GameObject itemUI;
    public GameObject checklistPanel;

    private void Awake() {
        Instance = this;
    }
}
