using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ========== Fields ==========

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text livesText;
    [SerializeField]
    Text gameOverText;

    int score = 0;
    int lives = 3;

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseLives()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        // effectively pauses the game
        if (lives <= 0)
        {
            Time.timeScale = 0;
            gameOverText.text = "Game Over";
        }
    }

}
