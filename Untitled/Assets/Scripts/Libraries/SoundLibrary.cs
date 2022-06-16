using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundLibrary : MonoBehaviour
{
    public AudioClip[] _sfxList2D;
    public static Dictionary<string ,AudioClip> sfxLibrary2D = new Dictionary<string, AudioClip>();

    public AudioClip[] _sfxList3D;
    public static Dictionary<string ,AudioClip> sfxLibrary3D = new Dictionary<string, AudioClip>();
    
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
    }

}
