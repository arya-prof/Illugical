using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor
{
    public bool itemContaine { get; }
    public bool doorOpened { get; }
    public void OnIntract();

    public string doorLockString { get; }
    public string doorContaineString { get; }

}
