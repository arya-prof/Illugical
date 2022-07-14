using Codice.CM.Common;
using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    private bool _lockOpened;
    private bool _doorOpened;
    
    [SerializeField] private Animator anim;
    [SerializeField] private Item item;
    public bool itemLock
    {
        get
        {
            if (item == null)
            {
                return false;
            }
            else
            {
                if (_lockOpened)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
    public bool itemContaine
    {
        get
        {
            if (References.Instance.playerBackpack.Contains(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool doorOpened 
    {
        get
        {
            return _doorOpened;
        }
    }

    public void OnIntract()
    {
        if (itemLock)
        {
            if (itemContaine)
            {
                References.Instance.playerBackpack.Remove(item);
                _lockOpened = true;
            }
        }
        else
        {
            _doorOpened = true;
            Debug.Log("opening");
        }
    }
}
