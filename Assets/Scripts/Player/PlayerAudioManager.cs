using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AudioPlay(AudioClip audioClip, float volume, bool randomize)
    {
        if(!randomize)
        {
            audioSource.pitch = 1;
            audioSource.PlayOneShot(audioClip,volume);
        } else
        {
            audioSource.pitch = (Random.Range(0.9f, 1.1f));
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
