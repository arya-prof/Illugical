
using UnityEngine;

public class FpsManager : MonoBehaviour
{
    // it will only limit fps when running game in unity editor
    void Awake () {
        // if you want it to also affect the game aswell comment # parts
        #if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 90;
        #endif
    }
}
