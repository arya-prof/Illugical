using UnityEngine;

public interface IHdCmStation
{
    public Transform sTransform { get; }
    public float sRadius { get; }
    public bool sActive { get; set; }
    public bool sFinish { get; set; }
    public void Activate();
}
