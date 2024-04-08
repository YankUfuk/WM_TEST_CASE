using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    // References
    [SerializeField] private GameObject[] enemyPrefabs;

    // Enemy spawn attributes
    [SerializeField] private int baseEnemies = 8; 
    [SerializeField] private float enemiesPerSecond = 0.5f; 
    [SerializeField] private float timeBetweenWaves = 5f; 
    [SerializeField] private float difficultyScalingFactor = 0.75f; 
    [SerializeField] private float enemiesPerSecondCap = 10f; 

    public int currentWave = 1; // Unlike others it is public because we have to reach it for ui
    private float timeSinceLastSpawn; // Time since the last enemy spawn
    private int enemiesAlive; 
    private int enemiesLeftToSpawn; 
    private float enemyPerSec; 
    private bool isSpawning = false; // Checks if currently spawning enemies

    // Events
    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Event triggered when an enemy is destroyed

    private void Awake()
    {
        // Event representing when an enemy is destroyed
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // Start the coroutine to begin spawning waves
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Checks if not currently spawning
        if (!isSpawning) return;

        // Update the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // If enough time has passed and there are still enemies left to spawn then spawn an enemy
        if (timeSinceLastSpawn >= (1f / enemyPerSec) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        // If all enemies are destroyed and none are left to spawn, end the wave
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    // Calculate the number of enemies to spawn in the current wave
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Calculate the spawn rate of enemies per second in the current wave
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    // End the current wave
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    // Spawn a single enemy
    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPosition.position, Quaternion.identity);
    }

    // Method called when an enemy is destroyed
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    // Coroutine to start a new wave
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemyPerSec = EnemiesPerSecond();
    }
}

