using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveHack : MonoBehaviour
{
    public TextMeshProUGUI hackText;
    public bool hacking;
    private float progress;
    public float speed = 3;
    public Transform[] spawns;
    public Transform enemyPrefab;
    private int previousEnemies;

    public void StartHack()
    {
        hackText.gameObject.SetActive(true);
        hacking = true;
        previousEnemies = EnemyManager.activeEnemies.Count;
    }

    private void Update() {
        if(!hacking) return;

        progress += Time.deltaTime * speed;

        hackText.text = "Hacking: " + (int)progress + "%";

        var i = 0;
        while (EnemyManager.activeEnemies.Count - previousEnemies < 2)
        {
            Instantiate(enemyPrefab, spawns[i].position, Quaternion.identity);
            i++;
        }
    }
}
