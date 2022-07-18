using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public static int completedLevel = -1;
    public int levelIndex;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            completedLevel = levelIndex;
            SceneManager.LoadScene(1);
        }
    }
}
