using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
[CreateAssetMenu(fileName = "Quest", menuName ="Quests/Quest")]
public class Quest : ScriptableObject
{

    [HideInInspector] public UnityEvent triggerEvent;
    [SerializeField, TextArea] public string description = "";

}
