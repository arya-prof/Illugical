using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public static List<Level> AllLevels = new List<Level>();
    public Transform levelsObject;

    private void Start() {

        for (int i = 0; i < levelsObject.childCount; i++){
            Level _lvl = levelsObject.GetChild(i).GetComponent<Level>();
            _lvl.index = i;
            AllLevels.Add(_lvl);
            if (0 < i){
                if (PlayerPrefs.GetInt("Level" + (i-1)) == 1){
                    _lvl.UnlockLevel();
                }
            }

            if (PlayerPrefs.HasKey("Level" + i))
                if (PlayerPrefs.GetInt("Level" + i) == 1)
                    _lvl.CompleteLevel();
            else
                PlayerPrefs.SetInt("Level" + i, 0);
        }
        PlayerPrefs.Save();
    }
    
}
