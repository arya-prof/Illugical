using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject master;
    
    [SerializeField] private TutorialBar[] bars;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private GameObject exitButton;
    private bool _closeAbleTime;
    
    private void Start()
    {
        master.SetActive(false);
        exitButton.SetActive(false);
    }

    public void OnActivate(int totIndex)
    {
        master.SetActive(true);
        if (totIndex < bars.Length)
        {
            References.Instance.freezWorld = true;
            titleText.text = bars[totIndex].title;
            float showTime = 2;
            bars[totIndex].master.SetActive(true);
            for (int i = 0; i < bars[totIndex].bars.Count; i++)
            {
                StartCoroutine(ActivateBar(totIndex, i, showTime));
                showTime += 3f;
            }
            StartCoroutine(ActiveCloseOption(showTime));
        }
    }

    IEnumerator ActivateBar(int totIndex, int barIndex, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        bars[totIndex].bars[barIndex].SetActive(true);
    }

    IEnumerator ActiveCloseOption(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        exitButton.SetActive(true);
        _closeAbleTime = true;
    }

    public void OnClose()
    {
        master.SetActive(false);
        
        exitButton.SetActive(false);
        _closeAbleTime = false;
        
        References.Instance.freezWorld = false;
    }

    private void Update()
    {
        if(!_closeAbleTime) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnClose();
        }
    }
}
