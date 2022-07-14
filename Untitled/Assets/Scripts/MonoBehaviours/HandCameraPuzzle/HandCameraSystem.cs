using UnityEngine;
using UnityEngine.Events;

public class HandCameraSystem : MonoBehaviour, IHdCmStation
{
    private SphereCollider collider;
    public Transform sTransform
    {
        get
        {
            return transform;
        }
    }
    public float sRadius
    {
        get
        {
            return collider.radius;
        }
    }

    private bool _sActive;
    public bool sActive
    {
        get
        {
            return _sActive;
        }
        set
        {
            _sActive = value;
        }
    }
    [SerializeField] private bool sActiveOnStart;

    private bool _sFinish;
    public bool sFinish
    {
        get
        {
            return _sFinish;
        }
        set
        {
            _sFinish = value;
        }
    }

    [SerializeField] private UnityEvent _event;

    public void Activate()
    {
        sFinish = true;
        _event?.Invoke();
    }

    private void Start()
    {
        collider = GetComponent<SphereCollider>();

        sActive = sActiveOnStart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandController.handController.handCameraStation = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandController.handController.handCameraStation = null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(1f,2f,1f);
        Vector3 center = transform.position + Vector3.up * 1f;
        Gizmos.DrawWireCube(center, size);
        
        Vector3 cameraCenter = transform.position + transform.up * 2f;
        Gizmos.DrawRay(cameraCenter,  transform.forward * 5f);
    }
}
