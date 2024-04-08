using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Player live attributes
    public static int lives;
    public int startLives = 20;

    private void Start()
    {
        
        lives = startLives; // Set the value of lives to the value of startLives when the game starts
    }

}
