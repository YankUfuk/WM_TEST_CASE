using UnityEngine;
using UnityEngine.UI;

public class WavesUI : MonoBehaviour
{
    public Text wavesText;
    private EnemySpawner enemySpawner;// we need this reference to reach the inside of enemySpawner

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Find the EnemySpawner object in the scene
    }

    void Update()
    {
        if (enemySpawner != null)
        {
            wavesText.text = "WAVE " + enemySpawner.currentWave.ToString();//takes currentwave value from enemy spawner and updates the text
        }
    }
}
