using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private Transform backgroundArea;
    [SerializeField] private Transform attackArea;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    private int choosenClip;

    private float _radios;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartAttack(float radios)
    {
        _radios = radios;

        choosenClip = Random.Range(0, clips.Length);
        
        backgroundArea.localScale = new Vector3(_radios,_radios,_radios);
        attackArea.localScale = new Vector3(1,1,1);
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(clips[choosenClip]);
    }

    public void UpdateData(float value)
    {
        float size = (value * _radios); 
        attackArea.localScale = new Vector3(size,size,size);
    }
}
