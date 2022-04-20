using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioManager : MonoBehaviour
{
    public AudioSource prefab;
    private AudioPool audioPool;

    private void Awake() {
        audioPool = new AudioPool(prefab);
    }

    public void AudioPlay(AudioClip audio, float volume, bool randomize, Vector3 pos)
    {
        AudioSource audioSource = audioPool.GetAudioSource(pos);

        if(!randomize)
        {
            audioSource.pitch = 1;
        } else
        {
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.2f);
        }

        audioSource.PlayOneShot(audio, volume);
    }
}
