using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int enemyWorth = 50;

    // this prevents the enemy number becomes below zero.
    private bool isDestroyed = false;

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(enemyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
