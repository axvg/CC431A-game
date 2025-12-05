using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    public Text livesText;
    public Text messageText;

    private int currentScore = 0;
    private int currentLives = 5;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
        if (messageText == null)
        {
            GameObject go = GameObject.Find("MessageText");
            if (go != null)
            {
                messageText = go.GetComponent<Text>();
            }
        }
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (currentLives <= 0)
        {
            if (!isGameOver)
            {
                isGameOver = true;
                if (messageText != null)
                {
                    messageText.gameObject.SetActive(true);
                    messageText.text = "GAME OVER\n but you can continue";
                    StartCoroutine(DisappearAfterSeconds(5f));
                }
            }
            return;
        }

        currentLives--; 
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver && messageText != null)
        {
            float hue = Mathf.PingPong(Time.time * 0.5f, 1f);
            messageText.color = Color.HSVToRGB(hue, 1f, 1f);
        }
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + currentScore;
        if (livesText != null) livesText.text = "Lives: " + currentLives;
    }

    IEnumerator DisappearAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (messageText != null)
        {
            messageText.text = "";
        }
    }   
}