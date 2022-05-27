using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    public static List<EnemyBase> activeEnemies = new List<EnemyBase>();
    public static int immaAttack = 0;
    
    public static void AddEnemy(EnemyBase enemy)
    {
        activeEnemies.Add(enemy);
        Debug.Log("Added a new enemy");
    }

    public static void RemoveEnemy(EnemyBase enemy)
    {
        activeEnemies.Remove(enemy);
        Debug.Log("Removed an enemy");  
    }

    public static void EnemyAttacking()
    {
        immaAttack++;
    }

    public static void EnemyStopsAttacking()
    {
        immaAttack--;
    }
}
