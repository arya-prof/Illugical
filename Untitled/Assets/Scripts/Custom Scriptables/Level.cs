using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Level", menuName ="Game/Level")]
public class Level : ScriptableObject{
    public Material textureMaterial;
    [TextArea] public string hoverText;
    [HideInInspector] public bool unlocked = false;
}
