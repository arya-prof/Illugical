using UnityEngine;
using UnityEngine.Events;

public class Func_EnterArea : MonoBehaviour
{
    [SerializeField] private UnityEvent enterEvent;
    [SerializeField] private UnityEvent exitEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exitEvent?.Invoke();
        }
    }
}
