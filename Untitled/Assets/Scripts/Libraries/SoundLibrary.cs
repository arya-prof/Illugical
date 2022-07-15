using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundLibrary : MonoBehaviour
{
    [HideInInspector] public AudioClip[] _sfxList2D;
    [HideInInspector] public static Dictionary<string ,AudioClip> sfxLibrary2D = new Dictionary<string, AudioClip>();

    [HideInInspector] public AudioClip[] _sfxList3D;
    [HideInInspector] public static Dictionary<string ,AudioClip> sfxLibrary3D = new Dictionary<string, AudioClip>();
    // Random SFX on play for 2D
    public string[] registered2D;
    [HideInInspector] public static Dictionary<string, AudioClip[]> sfxList2DRandom = new Dictionary<string, AudioClip[]>();
    // Random SFX on play for 3D
    public string[] registered3D;
    [HideInInspector] public static Dictionary<string, AudioClip[]> sfxList3DRandom = new Dictionary<string, AudioClip[]>();
    
    void Awake() {
        // 2D
        _sfxList2D = Resources.LoadAll("SFX2D" , typeof(AudioClip)).Cast<AudioClip>().ToArray(); // load all of sfx in resources folder ( thats why this folder is called resources )
        for (int i = 0; i < _sfxList2D.Length; i++){
            sfxLibrary2D.Add(_sfxList2D[i].name ,_sfxList2D[i]);
        }
        // 3D
        _sfxList3D = Resources.LoadAll("SFX3D" , typeof(AudioClip)).Cast<AudioClip>().ToArray(); // load all of sfx in resources folder ( thats why this folder is called resources )
        for (int i = 0; i < _sfxList3D.Length; i++){
            sfxLibrary3D.Add(_sfxList3D[i].name ,_sfxList3D[i]);
        }
        // 2D Random
        foreach (string folder in registered2D){
            AudioClip[] _sfxInFolder = Resources.LoadAll("SFX2D/" + folder, typeof(AudioClip)).Cast<AudioClip>().ToArray();
            sfxList2DRandom.Add(folder ,_sfxInFolder);
        }
        // 3D Random
        foreach (string folder in registered3D){
            AudioClip[] _sfxInFolder = Resources.LoadAll("SFX3D/" + folder, typeof(AudioClip)).Cast<AudioClip>().ToArray();
            sfxList3DRandom.Add(folder ,_sfxInFolder);
        }
    }

}
