using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]private GameObject cannonBall;
    [SerializeField] private Transform cannonShotPos;
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

    public void Fire()
    {
        GameObject ball = getCannonBall();
    }
}
