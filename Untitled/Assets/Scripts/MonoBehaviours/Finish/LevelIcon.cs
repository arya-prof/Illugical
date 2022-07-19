using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIcon : MonoBehaviour, ILevel
{
    [SerializeField] private GameObject lockObject;
    [SerializeField] private Renderer imageRenderer;
    [SerializeField] private Color playedColor;
    [SerializeField] private Color notPlayedColor;

    private bool _levelLock;
    public bool levelLock
    {
        get
        {
            return _levelLock;
        }
        set
        {
            _levelLock = value;
            if (!_levelLock)
            {
                lockObject.SetActive(false);
            }
            else
            {
                lockObject.SetActive(true);
            }
        }
    }

    private bool _doneBefore;
    public bool doneBefore
    {
        get
        {
            return _doneBefore;
        }
        set
        {
            _doneBefore = value;
            if (_doneBefore)
            {
                imageRenderer.material.SetColor("_Color", playedColor);
            }
            else
            {
                imageRenderer.material.SetColor("_Color", notPlayedColor);
            }
        }
    }

    private int _levelIndex;
    public int levelIndex
    {
        get
        {
            return _levelIndex;
        }
        set
        {
            _levelIndex = value;
        }
    }

    public void StartLevel()
    {
        int sceneValue = levelIndex + 2;
        SceneManager.LoadScene(sceneValue);
    }
}
