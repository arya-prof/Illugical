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
    
    private bool _open;
    public bool open
    {
        get
        {
            return _open;
        }
        set
        {
            _open = value;
        }
    }

    public void OnActivate()
    {
        if(!open) return;
        if (eventsIndex >= events.Length) return;

        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
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
    }
}
