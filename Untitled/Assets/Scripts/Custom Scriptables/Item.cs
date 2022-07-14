using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "BaseItem", menuName ="Item/BaseItem")]
public class Item : ScriptableObject
{
    [Header("Item Stats")]
    public string itemName = "Defulat Item";
    public Sprite itemIcon = null;
    [TextArea] public string description = "";

    [Header("If its not set , on pickup it will set rotation to 0")]
    public GameObject originalPrefab;
    
}
