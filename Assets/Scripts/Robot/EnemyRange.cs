using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public EnemyBase[] enemies;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.playerInRange = true;
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
        }
    }
}
