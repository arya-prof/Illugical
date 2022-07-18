using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IDoor
{
    private bool _doorOpened;
    
    [SerializeField] private UnityEvent openEvent;
    [SerializeField] private Item item;

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

    [SerializeField] private string _doorLockString;
    public string doorLockString
    {
        get { return _doorLockString; }
    }

    [SerializeField] private string _doorContaineString;
    public string doorContaineString
    {
        get { return _doorContaineString; }
    }

    public void OnIntract()
    {
        if (!_doorOpened)
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
                _doorOpened = true;
                openEvent?.Invoke();
            }
        }
    }
}
