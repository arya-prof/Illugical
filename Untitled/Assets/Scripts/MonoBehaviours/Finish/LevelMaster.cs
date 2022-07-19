using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    [SerializeField] private LevelIcon[] Levels;

    private void Awake()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].levelIndex = i;
            // Lock or Unlock
            if(i == 0)
            {
                Levels[0].levelLock = false;
            }
            else
            {
                int lastLevel = i - 1;
                string levelName = "lvl" + lastLevel;
                int value = PlayerPrefs.GetInt(levelName);
                if (value == 1)
                {
                    Levels[i].levelLock = false;
                }
                else
                {
                    Levels[i].levelLock = true;
                }
            }
            // Played
            string currentLevelName = "lvl" + i;
            int playedValue = PlayerPrefs.GetInt(currentLevelName);
            if (playedValue == 1)
            {
                Levels[i].doneBefore = true;
            }
            else
            {
                Levels[i].doneBefore = false;
            }
        }
    }
}
