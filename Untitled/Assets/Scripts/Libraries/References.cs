using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class References : MonoBehaviour
{
    public static References Instance { get; private set; }

    // Player
    public Transform cameraTransform;
    public GameObject otherCanvas;
    public List<Item> playerBackpack = new List<Item>();
    public List<GameObject> playerBackpackUI = new List<GameObject>();
    public CharacterController playerController;
    public LayerMask itemLayer;
    public AudioSource footstepsSource;
    private bool _freezWorld;
    public bool freezWorld
    {
        get
        {
            return _freezWorld;
        }
        set
        {
            _freezWorld = value;
        }
    }

    // UI
    public TMP_Text itemPopup;
    public GameObject inventoryPanel;
    public GameObject itemUI;
    public GameObject checklistPanel;
    // Local vars
    public bool playFootsteps = false;
    
    public IEnumerator PlayFootsteps(string folder, float delay, float volume, float variance){
        playFootsteps = true;
        AudioClip[] _sfxInFolder =  SoundLibrary.sfxList3DRandom[folder];
        AudioClip _sfx;
        while (playFootsteps){
            _sfx = _sfxInFolder[Random.Range(0,_sfxInFolder.Length)];

            footstepsSource.volume = volume;
            footstepsSource.pitch = 1 + Random.Range(-variance, variance);
            footstepsSource.PlayOneShot(_sfx);

            yield return new WaitForSeconds(_sfx.length + delay);
        }

    }

    public void StopFootsteps(){
        playFootsteps = false;
        footstepsSource.Stop();
    }

    private void Awake() {
        Instance = this;
    }
}
