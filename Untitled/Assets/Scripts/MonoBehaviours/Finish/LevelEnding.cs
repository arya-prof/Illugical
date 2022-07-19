using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnding : MonoBehaviour
{
    public int levelIndex;

    public void Finish()
    {
        string levelName = "lvl" + levelIndex;
        PlayerPrefs.SetInt(levelName,1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
