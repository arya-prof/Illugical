using UnityEngine;

public class Level : MonoBehaviour{
    public Color normalColor;
    public Color completedColor;
    public Texture levelTexture;
    public int sceneIndex;
    public bool unlocked = false;
    [TextArea] public string hoverText;
    [HideInInspector] public int index = 0;

    private void Awake() {
        if (unlocked) { UnlockLevel() ;}

        transform.GetChild(0).GetComponent<Renderer>().materials[1].mainTexture = levelTexture;
        Hoverable.CreateComponent(transform.gameObject, hoverText);
        transform.GetChild(0).GetComponent<Renderer>().materials[1].color = normalColor;
    }

    public void CompleteLevel(){
        if (!unlocked) { UnlockLevel() ;}

        transform.GetChild(0).GetComponent<Renderer>().materials[1].color = completedColor;
        PlayerPrefs.SetInt("Level" + index, 1);
        PlayerPrefs.Save();

    }
    
    public void UnlockLevel(){
        unlocked = true;
        // Remove the lock
        Destroy(transform.GetChild(2).gameObject);
    }
}
