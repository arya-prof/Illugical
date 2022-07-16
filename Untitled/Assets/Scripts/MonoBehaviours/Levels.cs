using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Level", menuName ="Game/Level")]
public class Level : ScriptableObject{
    public Sprite image;
    [TextArea] public string hoverText;
    public int index;
    [HideInInspector] public bool unlocked;
}

public class Levels : MonoBehaviour
{
    public Level[] AllLevels;
    public GameObject[] LevelObjects;

    private void Awake() {
        for (int i =0; i < AllLevels.Length; i++){
            
        }
    }
    
}
