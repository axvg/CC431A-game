using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    private int currScore = 0;

    void Awake()
    {
        Instance = this;
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        currScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currScore.ToString();
        }
    }
}