using Codice.CM.Common;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IDoor
{
    private bool _lockOpened;
    private bool _doorOpened;
    
    [SerializeField] private UnityEvent openEvent;
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
                for (int i = 0; i < References.Instance.playerBackpack.Count; i++)
                {
                    if (item == References.Instance.playerBackpack[i])
                    {
                        References.Instance.playerBackpack.RemoveAt(i);
                        Destroy(References.Instance.playerBackpackUI[i]);
                        References.Instance.playerBackpackUI.RemoveAt(i);
                    }
                }
                _lockOpened = true;
            }
        }
        else
        {
            _doorOpened = true;
            openEvent?.Invoke();
        }
    }
}
