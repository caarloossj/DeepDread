using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class WaveHack : MonoBehaviour
{
    public TextMeshProUGUI hackText;
    public bool hacking;
    private float progress;
    public float speed = 3;
    public Transform[] spawns;
    public Transform enemyPrefab;
    private int previousEnemies;
    public bool hackComplete = false;
    public bool bridgeOpened = false;
    public PlayableDirector playableDirector;
    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    public void StartHack()
    {
        hackText.gameObject.SetActive(true);
        hacking = true;
        previousEnemies = EnemyManager.activeEnemies.Count;
        float vol = PlayerPrefs.GetInt("musicVolume", 1) / 10f * 0.2f;
        musicSource.DOFade(vol, 1);
    }

    private void EndHack()
    {
        FindObjectOfType<RobotFollower>().target = ActionCharacter.Instance.transform.Find("RobotTarget").transform;
        hackText.text = "Status: Complete \n Kill the enemies";
        hacking = false;
        hackComplete = true;
        FindObjectOfType<FloatingBox>().Popup("Listo! He terminado");
    }

    public void ResetHack()
    {
        if(bridgeOpened) return;
        hacking = false;
        hackComplete = false;
        hackText.gameObject.SetActive(false);
        progress = 0;
        FindObjectOfType<RobotFollower>().target = ActionCharacter.Instance.transform.Find("RobotTarget").transform;
    }

    private void Update() {
        if(hackComplete)
        {
            if(EnemyManager.activeEnemies.Count <= (GameManager.Instance.firstEnemy ? 0 : 1))
            {
                musicSource.DOFade(0, 2);
                bridgeOpened = true;
                playableDirector.Play();
                hackText.gameObject.SetActive(false);
                hackComplete = false;
            }
        }

        if(!hacking) return;

        progress += Time.deltaTime * speed;

        if(progress >= 100)
        {
            EndHack();
            return;
        }

        hackText.text = "Status: " + (int)progress + "%";

        var i = 0;
        while (EnemyManager.activeEnemies.Count - previousEnemies < 2 && progress < 70)
        {
            Instantiate(enemyPrefab, spawns[i].position, Quaternion.identity);
            i++;
        }
    }
}
