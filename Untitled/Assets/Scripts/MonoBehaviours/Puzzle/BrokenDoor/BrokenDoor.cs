using UnityEngine;
using UnityEngine.Events;

public class BrokenDoor : MonoBehaviour, IDoor
{
    private bool _doorOpened;
    private int completedPieces = 0;
    
    [SerializeField] private UnityEvent openEvent;
    [SerializeField] private DoorPiece[] doorPieces;

    // No Usage
    public bool itemContaine
    {
        get{
            return true;
        }
    }
    public bool doorOpened 
    {
        get
        {
            return completedPieces == doorPieces.Length;
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
            for (int i = 0; i < References.Instance.playerBackpack.Count; i++)
            {
                for (int j = 0; j < doorPieces.Length; j++){
                    
                    if (doorPieces[j]._item == References.Instance.playerBackpack[i])
                    {
                        References.Instance.playerBackpack.RemoveAt(i);
                        Destroy(References.Instance.playerBackpackUI[i]);
                        References.Instance.playerBackpackUI.RemoveAt(i);
                        doorPieces[j].gameObject.SetActive(true);
                        completedPieces++;
                        if (completedPieces == doorPieces.Length){
                            openEvent?.Invoke();
                        }
                        return;
                    }

                }
            }
            
        }
    }
}
