using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [SerializeField] private UnityEvent doneEvent;
    //
    private bool _gearA;
    public bool gearA
    {
        get
        {
            return _gearA;
        }
        set
        {
            _gearA = value;
            Check();
        }
    }
    //
    private bool _gearB;
    public bool gearB
    {
        get
        {
            return _gearB;
        }
        set
        {
            _gearB = value;
            Check();
        }
    }
    //
    private bool _battery;
    public bool battery
    {
        get
        {
            return _battery;
        }
        set
        {
            _battery = value;
            Check();
        }
    }
    //
    private void Check()
    {
        if (gearA && gearB && battery)
        {
            doneEvent?.Invoke();
        }
    }
}
