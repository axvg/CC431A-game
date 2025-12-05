using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PatternController : MonoBehaviour
{
    [Header("levels config")]
    public List<LevelData> gameLevels;
    
    [Header("UI References")]
    public Text messageText; 
    public Text timerText;

    private float levelTotalTime = 0f;
    private int lastSeconds = -1;

    [Header("curr state")]
    public int currentLevelIndex = 0;
    
    private LevelData currentLevelData;
    private int currentWaveIndex = 0;
    private float waveTimer;
    private float spawnTimer;
    private float xSpawnLimit = 8f;
    private bool isGameActive = false;

    private int spawnIndex = 0;

    void Start()
    {
        if (messageText == null)
        {
            Debug.LogError("missing messageText");
            return;
        }

        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        isGameActive = false;
        Time.timeScale = 1;
        
        messageText.text = "GET READY";
        yield return new WaitForSeconds(1f);

        messageText.text = "3";
        yield return new WaitForSeconds(1f);

        messageText.text = "2";
        yield return new WaitForSeconds(1f);

        messageText.text = "1";
        yield return new WaitForSeconds(1f);

        messageText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        messageText.text = "";
        
        LoadLevel(0);
        isGameActive = true;
    }

    void LoadLevel(int index)
    {
        if (index >= gameLevels.Count)
        {
            StartCoroutine(VictorySequence());
            currentLevelData = null;
            return;
        }

        currentLevelIndex = index;
        currentLevelData = gameLevels[index];
        
        currentWaveIndex = 0;
        waveTimer = 0;
        spawnTimer = 0;

        Debug.Log("start lvl: " + (index + 1));

        levelTotalTime = 0f;
        StartCoroutine(ShowLevelName(index + 1));
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ConfigureAbilities(currentLevelData.playerColors, currentLevelData.allowFocus);
        }
    }

    void Update()
    {
        if (!isGameActive || currentLevelData == null) return;


        levelTotalTime += Time.deltaTime;
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(levelTotalTime / 60F);
            int seconds = Mathf.FloorToInt(levelTotalTime % 60F);
            
            if (seconds != lastSeconds)
            {
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                lastSeconds = seconds;
            }
        }
        if (currentLevelData.waves.Count == 0) return;

        Wave currentWave = currentLevelData.waves[currentWaveIndex];

        // 1. spawning
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.spawnRate)
        {
            if (currentWave.enemyOptions.Length > 0)
            {
                int r = Random.Range(0, currentWave.enemyOptions.Length);
                SpawnEnemy(currentWave.enemyOptions[r], currentWave);
            }
            spawnTimer = 0f;
        }

        // 2. control wave duration
        waveTimer += Time.deltaTime;
        if (waveTimer >= currentWave.duration)
        {
            NextWave();
        }
    }

    void SpawnEnemy(GameObject prefab, Wave waveData)
    {
        if (prefab == null) return;
        float finalX = 0f;

        if (waveData.spawnXPositions != null && waveData.spawnXPositions.Length > 0)
        {
            finalX = waveData.spawnXPositions[spawnIndex];
            spawnIndex = (spawnIndex + 1) % waveData.spawnXPositions.Length;
        }
        else
        {
            finalX = Random.Range(-xSpawnLimit, xSpawnLimit);
        }
        Vector3 spawnPos = new Vector3(finalX, 6f, 0);
        
        // GameObject newEnemyObj = Instantiate(prefab, spawnPos, prefab.transform.rotation);
        GameObject newEnemyObj = Instantiate(prefab, spawnPos, Quaternion.Euler(0, 0, 180));

        BulletColor chosenColor = BulletColor.Red;
        
        if (currentLevelData.colorMode == LevelColorMode.Constant)
        {
            chosenColor = currentLevelData.constantColor;
        }
        else
        {
            if (currentLevelData.randomColors.Length > 0)
            {
                int r = Random.Range(0, currentLevelData.randomColors.Length);
                chosenColor = currentLevelData.randomColors[r];
            }
        }

        EnemyController enemyScript = newEnemyObj.GetComponent<EnemyController>();
        if (enemyScript != null)
        {
            enemyScript.SetEnemyColor(chosenColor);
        }
    }

    void NextWave()
    {
        spawnIndex = 0;
        waveTimer = 0;
        currentWaveIndex++;

        if (currentWaveIndex >= currentLevelData.waves.Count)
        {
            StartCoroutine(LevelCompleteSequence());
        }
    }

    IEnumerator LevelCompleteSequence()
    {
        isGameActive = false;
        
        messageText.text = "LEVEL COMPLETE!";
        
        Time.timeScale = 0; // sstop time

        yield return new WaitForSecondsRealtime(3f);

        messageText.text = "";
        
        Time.timeScale = 1; 
        
        LoadLevel(currentLevelIndex + 1);
        
        isGameActive = true;
    }

    IEnumerator VictorySequence()
    {
        messageText.text = "YOU WIN!";
        Time.timeScale = 0; // freeze forever !!!!!
        yield return null;
    }

    IEnumerator ShowLevelName(int levelNumber)
    {
        messageText.text = "LEVEL " + levelNumber;
        messageText.fontSize = 60;
        yield return new WaitForSeconds(2f);
        
        if (!string.IsNullOrEmpty(currentLevelData.startMessage))
        {
            messageText.text = currentLevelData.startMessage;
            messageText.fontSize = 40;
            
            yield return new WaitForSeconds(3f);
        }
        
        messageText.text = "";
    }
}