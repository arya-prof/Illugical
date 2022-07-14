using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntract
{
    public void OnActivate();
    public bool open { get; set; }
}
