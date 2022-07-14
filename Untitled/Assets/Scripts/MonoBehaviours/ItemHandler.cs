using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Item item;

    public void Pickup(){
        References.Instance.playerBackpack.Add(item);
        Destroy(this.gameObject);
        AddToUI();
    }

    private void AddToUI(){
        GameObject uiObject = Instantiate(References.Instance.itemUI, References.Instance.inventoryPanel.transform);
        References.Instance.playerBackpackUI.Add(uiObject);
        uiObject.GetComponent<Image>().sprite = item.itemIcon;
    }
}
