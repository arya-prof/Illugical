using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public int levelIndex;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            SceneManager.LoadScene(1);
            Levels.AllLevels[levelIndex].CompleteLevel();
        }
    }
}
