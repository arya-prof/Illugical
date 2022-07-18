using UnityEngine;

public class Level : MonoBehaviour{
    public Texture levelTexture;
    public int sceneIndex;
    public bool unlocked = false;
    [TextArea] public string hoverText;
    [HideInInspector] public int index = 0;

    private void Start() {
        if (unlocked) { UnlockLevel() ;}

        transform.GetChild(0).GetComponent<Renderer>().materials[1].mainTexture = levelTexture;
        Hoverable.CreateComponent(transform.gameObject, hoverText);
        transform.GetChild(0).GetComponent<Renderer>().materials[1].color = Color.gray;
    }

    public void CompleteLevel(){
        if (!unlocked) { UnlockLevel() ;}
        
        transform.GetChild(0).GetComponent<Renderer>().materials[1].color = Color.green;
        PlayerPrefs.SetInt("Level" + index, 1);
        PlayerPrefs.Save();

    }
    
    public void UnlockLevel(){
        unlocked = true;
        // Remove the lock
        Destroy(transform.GetChild(2).gameObject);
    }
}
