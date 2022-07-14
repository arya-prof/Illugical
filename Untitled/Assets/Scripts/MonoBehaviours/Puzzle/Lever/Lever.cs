using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IIntract
{
    [SerializeField] private UnityEvent[] events;
    [SerializeField] private int eventsIndex;
    [SerializeField] private bool repeatAble;
    [SerializeField] private Animator anim;
    [SerializeField] private float animTime;

    private bool _delay;
    private bool _open;
    public bool open
    {
        get
        {
            if (!_delay || _open)
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

    public void OnActivate()
    {
        if (_delay) return;
        if(!open) return;
        if (eventsIndex >= events.Length) return;

        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        _delay = true;
        
        events[eventsIndex]?.Invoke();
        anim.SetTrigger("activate");
        yield return new WaitForSeconds(animTime);
        eventsIndex++;
        if (eventsIndex >= events.Length)
        {
            if (repeatAble)
            {
                eventsIndex = 0;
            }
        }

        _delay = false;
    }
}
