using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Func_Time : MonoBehaviour
{
    [SerializeField] private bool activeOnStart;
    [SerializeField] private float startDelay;
    
    [SerializeField] private UnityEvent activeEvent;

    private void Start()
    {
        if (activeOnStart)
        {
            StartCoroutine(Active());
        }
    }

    public void OnActive()
    {
        StartCoroutine(Active());
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(startDelay);
        activeEvent?.Invoke();
    }
}
