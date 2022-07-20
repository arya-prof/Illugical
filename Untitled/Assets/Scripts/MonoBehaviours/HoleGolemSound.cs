using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class HoleGolemSound : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void HgolemSound()
    {
            audioSource.PlayOneShot(impact, 1f);
            CameraShaker.Instance.ShakeOnce(4f, 4f, 2f, 15f);
    }
}
