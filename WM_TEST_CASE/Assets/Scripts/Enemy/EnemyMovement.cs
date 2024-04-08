using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Enemy movement attributes
    [SerializeField] public float moveSpeed = 2f; 
    private int pathIndex = 0; 
    private float baseSpeed; 

    // References
    [SerializeField] private Rigidbody2D rb; // Rigidbody component of the enemy

    private Transform target; // Target position to move towards
    

    private void Start()
    {
        baseSpeed = moveSpeed; // Set baseSpeed to the initial movement speed
        target = LevelManager.main.path[pathIndex]; // Set initial target position
    }

    private void Update()
    {
        // Check if enemy has reached the current target position
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // Move to the next path node

            // Check if reached the end of the path
            if (pathIndex == LevelManager.main.path.Length)
            {
                PlayerStats.lives--; // If an enemy reaches the end of the path then decrease player's lives
                EnemySpawner.onEnemyDestroy.Invoke(); 
                Destroy(gameObject); // Destroys enemy
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex]; // Set the next target position
            }
        }
    }

    private void FixedUpdate()
    {
        // Calculates the direction towards the target position
        Vector2 direction = (target.position - transform.position).normalized;

        // Apply movement using Rigidbody velocity
        rb.velocity = direction * moveSpeed;
    }

    // Method to update the movement speed of the enemy
    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed; 
    }

    // Method to reset the movement speed of the enemy to its base speed
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed; 
    }
}

