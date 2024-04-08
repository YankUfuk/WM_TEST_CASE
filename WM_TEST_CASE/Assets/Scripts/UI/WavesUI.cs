using UnityEngine;
using UnityEngine.UI;

public class WavesUI : MonoBehaviour
{
    public Text wavesText;
    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Find the EnemySpawner object in the scene
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner not found in the scene.");
        }
    }

    void Update()
    {
        if (enemySpawner != null)
        {
            wavesText.text = "WAVE " + enemySpawner.currentWave.ToString();
        }
        else
        {
            wavesText.text = "WAVE N/A"; // Show a placeholder if EnemySpawner is not found
        }
    }
}
