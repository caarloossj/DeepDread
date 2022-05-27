using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public AudioClip Ataque;
    public AudioClip Caer;
    public AudioClip Paso;
    public AudioClip Grunt1;
    public AudioClip Grunt2;
    public AudioClip Grito1;
    public AudioClip Grito2;
    private Dictionary<string,AudioClip> soundDict = new Dictionary<string, AudioClip>();
    private GlobalAudioManager audioManager;

    void Awake()
    {
        soundDict.Add("attack", Ataque);
        soundDict.Add("caer", Caer);
        soundDict.Add("paso", Paso);
        soundDict.Add("grunt1", Grunt1);
        soundDict.Add("grunt2", Grunt2);
        soundDict.Add("grito1", Grito1);
        soundDict.Add("grito2", Grito2);

        audioManager = FindObjectOfType<GlobalAudioManager>();
    }

    public void AudioPlay(string audio)
    {
        string[] str = audio.Split('_');

        audioManager.AudioPlay(soundDict[str[0]], float.Parse(str[1]), str[2] == "1", transform.position);
    }    
}
