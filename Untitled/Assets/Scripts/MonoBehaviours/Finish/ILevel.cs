using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevel 
{
    public bool levelLock { get; set; }
    public void StartLevel();
}
