using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text description; 
    [SerializeField] private GameObject checkBox;
    [SerializeField] private Quest _quest;

    private void Awake() {
        _quest.triggerEvent = new UnityEngine.Events.UnityEvent();
        _quest.triggerEvent.AddListener(FinishQuest);
        description.text = _quest.description;
    }

    private void FinishQuest() {
        Debug.Log("Quest is finished!");
    }
}
