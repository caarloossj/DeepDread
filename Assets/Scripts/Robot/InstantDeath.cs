using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    public AudioClip clip;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<GlobalAudioManager>().AudioPlay(clip, 0.8f, false, transform.position, false);
            FindObjectOfType<GameManager>().Die(0);
        }
    }
}
