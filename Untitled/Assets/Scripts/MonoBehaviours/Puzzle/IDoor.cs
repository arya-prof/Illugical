using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor
{
    public bool itemLock { get; }
    public bool itemContaine { get; }
    public bool doorOpened { get; }
    public void OnIntract();
}
