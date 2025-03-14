using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroup : MonoBehaviour
{
    public GameObject gameOverUI;
    public DestroyableObject[] enemies;
    //
    private int enemiesCount;

    private void Awake()
    {
        enemiesCount = enemies.Length;
        foreach (DestroyableObject enemy in enemies)
        {
            enemy.onEnemyDie += EnemyDied;
        }
    }

    private void EnemyDied()
    {
        enemiesCount--;
        Debug.Log($"Enemy died. {enemiesCount} are left");
        if ( enemiesCount == 0)
        {
            DoGameOver();
        }
    }

    private void DoGameOver()
    {
        gameOverUI.SetActive(true);
    }
}
