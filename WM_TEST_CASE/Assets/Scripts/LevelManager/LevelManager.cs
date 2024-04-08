using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPosition;
    public Transform[] path;

    private bool gameEnded = false;
    public GameObject gameOverUI;

    public int currency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
    }
    private void Update()
    {
        if (gameEnded) return;

        if(PlayerStats.lives <= 0)
        {
            EndGame();
        }

    }

    private void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
