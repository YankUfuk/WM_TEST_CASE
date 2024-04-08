using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Enemy health and price attributes
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int enemyWorth = 25;

    // This prevents the enemy number becomes below zero
    private bool isDestroyed = false;

    // Method to make enemy take damage
    public void TakeDamage(int damage)
    {
        hitPoints -= damage; // Reduce enemy hit points

        // Check if enemy is destroyed
        if (hitPoints <= 0 && !isDestroyed)
        {
            // Invoke event for enemy destruction
            EnemySpawner.onEnemyDestroy.Invoke();
            // Increase money when enemy is destroyed
            LevelManager.main.IncreaseCurrency(enemyWorth);
            
            // Destroy attributes
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}

