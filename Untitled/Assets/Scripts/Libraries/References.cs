using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class References : MonoBehaviour
{
    public static References Instance { get; private set; }

    // Player
    public Transform cameraTransform;
    public GameObject mainCanvas;
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
    public GameObject itemPopupE;
    public TMP_Text itemPopup;
    public GameObject inventoryPanel;
    public GameObject itemUI;
    public GameObject checklistPanel;
    public GameObject CreditsUI;
    // Local vars    
    public IEnumerator PlayFootsteps(string folder, float delay, float volume, float variance){
        AudioClip[] _sfxInFolder =  SoundLibrary.sfxList3DRandom[folder];
        AudioClip _sfx;
        while (true){
            _sfx = _sfxInFolder[Random.Range(0,_sfxInFolder.Length)];

            footstepsSource.volume = volume;
            footstepsSource.pitch = 1 + Random.Range(-variance, variance);
            footstepsSource.PlayOneShot(_sfx);

            yield return new WaitForSeconds(_sfx.length + delay);
        }

    }

    // Fade
    public Image fadeImage;
    public bool fadeValue;

    public UnityEvent endGameEvent;
    
    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(Fade());
    }

    public void EndGame()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        if (!fadeValue)
        {
            freezWorld = true;
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            } 
            freezWorld = false;
            fadeValue = true;
        }
        else
        {
            freezWorld = true;
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
            endGameEvent?.Invoke();
        }
    }
}
