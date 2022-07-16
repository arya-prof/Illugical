using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    public string hoverText = "";
    public static Hoverable CreateComponent(GameObject obj, string hoverText){
        Hoverable _hov = obj.AddComponent<Hoverable>();
        _hov.hoverText = hoverText;
        return _hov;
    }
}
