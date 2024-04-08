using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main; // Singleton instance of the LevelManager

    // Enemy pathfinding attributes
    public Transform startPosition; 
    public Transform[] path; 

    //UI Objects for displaying when the game is ended.
    public GameObject gameOverUI; 
    public GameObject winningUI; 

    private bool gameEnded = false;// Checks whether the game is ended or not

    private EnemySpawner enemySpawner; // Enemy spawner reference

    public int currency; 

    private void Awake()
    {
        main = this; // Assigning the singleton instance of LevelManager
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Finding the EnemySpawner component in the scene
        currency = 100; 
    }

    private void Update()
    {
        if (gameEnded) return; // If the game has ended, exit the update loop

        // Check if the player has run out of lives
        if (PlayerStats.lives <= 0)
        {
            EndGame(); 
            GetComponent<EnemySpawner>().enabled = false; // Stop enemy spawning
        }

        // If player can survive for 20 waves
        if (enemySpawner.currentWave == 20)
        {
            WinGame(); 
            GetComponent<EnemySpawner>().enabled = false; // Stop enemy spawning
        }
    }

    private void WinGame()
    {
        gameEnded = true; 
        winningUI.SetActive(true); // Activate the winning UI
        
    }

    private void EndGame()
    {
        gameEnded = true; 
        gameOverUI.SetActive(true); // Activate the game over UI
        
    }

    // Method for increasing currency
    public void IncreaseCurrency(int amount)
    {
        currency += amount; 
    }

    // Method for spending currency
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency) // Check if the player has enough currency
        {
            currency -= amount; // Subtract spent amount from currency
            return true; 
        }
        else
        {
            return false; // Return false indicating insufficient currency
        }
    }
}
