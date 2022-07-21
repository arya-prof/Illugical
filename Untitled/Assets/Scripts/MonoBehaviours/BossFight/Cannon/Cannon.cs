using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Item cannonItem;
    [SerializeField]private GameObject cannonBall;
    [SerializeField] private Transform cannonShotPos;
    private AudioSource audioSource;
    [SerializeField] private AudioClip fireClip;
    private GameObject activeCannonBall;
    private GameObject getCannonBall()
    {
        if (activeCannonBall != null)
        {
            GameObject oldBall = activeCannonBall;
            oldBall.transform.position = cannonShotPos.position;
            oldBall.SetActive(true);
            return oldBall;
        }
        else
        {
            GameObject newBall = Instantiate(cannonBall,cannonShotPos.position, Quaternion.identity);
            return newBall;
        }
    }
    
    public bool fire
    {
        get
        {
            if (References.Instance.playerBackpack.Contains(cannonItem))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        audioSource.PlayOneShot(fireClip);
        EZCameraShake.CameraShaker.Instance.ShakeOnce(3f, 3f, 0.1f, 2f);
        for (int i = 0; i < References.Instance.playerBackpack.Count; i++)
        {
            if (cannonItem == References.Instance.playerBackpack[i])
            {
                References.Instance.playerBackpack.RemoveAt(i);
                Destroy(References.Instance.playerBackpackUI[i]);
                References.Instance.playerBackpackUI.RemoveAt(i);
            }
        }
        GameObject ball = getCannonBall();
    }
}
