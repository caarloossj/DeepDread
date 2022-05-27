using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    public AudioClip Grunt1;
    public AudioClip Grunt2;
    public AudioClip Grunt3;
    public AudioClip Grunt4;
    public AudioClip Barrido1;
    public AudioClip Barrido2;
    public AudioClip Golpe1;
    public AudioClip Golpe2;
    private Dictionary<string,AudioClip> soundDict = new Dictionary<string, AudioClip>();
    private GlobalAudioManager audioManager;

    void Awake()
    {
        soundDict.Add("grunt1", Grunt1);
        soundDict.Add("grunt2", Grunt2);
        soundDict.Add("grunt3", Grunt3);
        soundDict.Add("grunt4", Grunt4);
        soundDict.Add("golpe1", Golpe1);
        soundDict.Add("golpe2", Golpe2);
        soundDict.Add("barrido1", Barrido1);
        soundDict.Add("barrido2", Barrido2);

        audioManager = FindObjectOfType<GlobalAudioManager>();
    }

    public void AudioPlay(string audio)
    {
        string[] str = audio.Split('_');

        audioManager.AudioPlay(soundDict[str[0]], float.Parse(str[1]), str[2] == "1", transform.position);
    }    
}
