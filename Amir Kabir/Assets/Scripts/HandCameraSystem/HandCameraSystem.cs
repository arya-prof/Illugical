using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCameraSystem : MonoBehaviour, IHdCmStation
{

    public Transform sTransform
    {
        get
        {
            return transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandController.handController.handCameraStation = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandController.handController.handCameraStation = null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(1f,2f,1f);
        Vector3 center = transform.position + Vector3.up * 1f;
        Gizmos.DrawWireCube(center, size);
        
        Vector3 cameraCenter = transform.position + transform.up * 2f;
        Gizmos.DrawRay(cameraCenter,  transform.forward * 5f);
    }
}
