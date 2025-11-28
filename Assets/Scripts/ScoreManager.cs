using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    public Text livesText;

    private int currentScore = 0;
    private int currentLives = 5;

    void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    public void LoseLife()
    {
        currentLives--; 
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
        if (livesText != null) livesText.text = "Lives: " + currentLives;
    }
}