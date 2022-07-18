using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IIntract
{
    [SerializeField] private UnityEvent[] startEvent;
    [SerializeField] private UnityEvent[] activeEvent;
    
    private int eventsIndex;
    [SerializeField] private bool repeatAble;
    [SerializeField] private Animator anim;
    
    [SerializeField] private float activeTime;
    [SerializeField] private float reloadTime;

    private bool _delay;
    private bool _open;
    public bool open
    {
        get
        {
            if (!_delay && _open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        set
        {
            _open = value;
        }
    }
    [SerializeField] private bool openInStart;

    private void Start()
    {
        open = openInStart;
    }

    public void OnActivate()
    {
        if (_delay) return;
        if(!open) return;
        if (eventsIndex >= activeEvent.Length) return;

        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        _delay = true;
        //Start
        startEvent[eventsIndex]?.Invoke();
        anim.SetTrigger("active");
        //Active
        yield return new WaitForSeconds(activeTime);
        activeEvent[eventsIndex]?.Invoke();
        eventsIndex++;
        if (eventsIndex >= activeEvent.Length)
        {
            if (repeatAble)
            {
                eventsIndex = 0;
            }
        }
        //Done
        yield return new WaitForSeconds(reloadTime);
        _delay = false;
    }
}
