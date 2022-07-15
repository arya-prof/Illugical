using UnityEngine;
using UnityEngine.Events;

public class Func_EnterArea : MonoBehaviour
{
    [SerializeField] private UnityEvent enterEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
