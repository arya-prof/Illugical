using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private Item item;

    public void Pickup(){
        References.Instance.playerBackpack.Add(item);
        Destroy(this.gameObject);
        OnPickup();
    }

    private void OnPickup(){

    }
}
