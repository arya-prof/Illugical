using UnityEngine;
using System.Linq;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private static GameObject soundObject2D;
    [SerializeField] private static GameObject soundObject3D;

    private void Awake() {
        soundObject2D = Resources.Load<GameObject>("SoundObjects/SoundObject2D");
        soundObject3D = Resources.Load<GameObject>("SoundObjects/SoundObject3D");
    }
    public static void PlaySound2D(AudioClip soundClip, float volume = 0.4f, float variance = 0.2f) {
        GameObject newSound = Instantiate(soundObject2D);
        AudioSource soundSource = newSound.GetComponent<AudioSource>();

        soundSource.volume = volume;
        soundSource.pitch += Random.Range(-variance, variance);

        soundSource.PlayOneShot(soundClip);

        Destroy(newSound, soundClip.length);
    }

    public static void PlaySound3D(AudioClip soundClip, Vector3 position, float volume = 0.4f, float variance = 0.2f) {
        GameObject newSound = Instantiate(soundObject3D , position , Quaternion.Euler(0, 0, 0));
        AudioSource soundSource = newSound.GetComponent<AudioSource>();

        soundSource.volume = volume;
        soundSource.pitch += Random.Range(-variance, variance);

        soundSource.PlayOneShot(soundClip);

        Destroy(newSound, soundClip.length);
    }

}
