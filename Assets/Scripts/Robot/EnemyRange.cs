using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyRange : MonoBehaviour
{
    public EnemyBase[] enemies;
    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.playerInRange = true;
            }

            if(EnemyManager.activeEnemies.Count > 0)
            {
                float vol = PlayerPrefs.GetInt("musicVolume", 1) / 10f * 0.2f;
                musicSource.DOFade(vol, 1);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.playerInRange = false;
            }
            musicSource.DOFade(0, 2);
        }
    }
}
