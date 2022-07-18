using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public static List<Level> AllLevels = new List<Level>();

    private void Awake() {
        for (int i = 0; i < transform.childCount; i++){
            Level _lvl = transform.GetChild(i).GetComponent<Level>();
            _lvl.index = i;
            AllLevels.Add(_lvl);
            if (0 < i){
                if (PlayerPrefs.GetInt("Level" + (i-1)) == 1){
                    _lvl.UnlockLevel();
                }
            }

            if (!PlayerPrefs.HasKey("Level" + i))
                PlayerPrefs.SetInt("Level" + i, 0);
            else
                if (PlayerPrefs.GetInt("Level" + i) == 1)
                    _lvl.CompleteLevel();
        }
        PlayerPrefs.Save();
    }
    
}
