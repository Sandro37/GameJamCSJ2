using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] sonsEfeitos;
    public AudioClip[] sonsPassos;
    public AudioClip[] sonsBackground;
    public static AudioController instace;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instace = this;
    }

    public void TocarAudio(AudioClip music, float tamanho = 1)
    {
        audioSource.PlayOneShot(music, tamanho);
    }

    public bool VericarSeAudioEstaTocando()
    {
        return audioSource.isPlaying;
    }


    public void PlaySoundSFX(int index)
    {
        if (AudioController.instace.sonsEfeitos.Length > 0)
            AudioController.instace.TocarAudio(AudioController.instace.sonsEfeitos[index], 1f);
    }
}
