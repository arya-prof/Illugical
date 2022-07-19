using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEnding : MonoBehaviour
{
    public int levelIndex;

    public void Finish()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        string levelName = "lvl" + levelIndex;
        PlayerPrefs.SetInt(levelName,1);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);
    }
}
