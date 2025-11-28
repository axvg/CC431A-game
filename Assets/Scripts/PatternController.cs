using UnityEngine;
using System.Collections.Generic;

// [System.Serializable] 
// public class Wave
// {
//     public string name;
//     // public BasePattern pattern;
//     public GameObject[] enemyOptions;
//     public float duration;
//     public float spawnRate;
// }

public class PatternController : MonoBehaviour
{
[Header("Configuración de Niveles")]
    public List<LevelData> gameLevels;
    public bool loopGame = false;

    [Header("Estado Actual (No tocar)")]
    public int currentLevelIndex = 0;
    private int currentWaveIndex = 0;
    private LevelData currentLevelData;

    private float waveTimer;
    private float spawnTimer;
    public float xSpawnLimit = 8f;

    void Start()
    {
        LoadLevel(0);
    }

    void LoadLevel(int index)
    {
        if (index >= gameLevels.Count)
        {
            Debug.Log("end game");
            currentLevelData = null; // stop all
            return;
        }

        currentLevelIndex = index;
        currentLevelData = gameLevels[index];
        
        currentWaveIndex = 0;
        waveTimer = 0;
        spawnTimer = 0;
        
        Debug.Log("Iniciando: " + currentLevelData.levelName);
    }

    void Update()
    {
        if (currentLevelData == null) return;

        if (currentLevelData.waves.Count == 0) return;

        Wave currentWave = currentLevelData.waves[currentWaveIndex];

        // 1. spawning
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.spawnRate)
        {
            if (currentWave.enemyOptions.Length > 0)
            {
                int r = Random.Range(0, currentWave.enemyOptions.Length);
                SpawnEnemy(currentWave.enemyOptions[r]);
            }
            spawnTimer = 0f;
        }

        // 2. control de tiempo de oleada
        waveTimer += Time.deltaTime;
        if (waveTimer >= currentWave.duration)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        waveTimer = 0;
        currentWaveIndex++;

        // if all waves completed
        if (currentWaveIndex >= currentLevelData.waves.Count)
        {
            Debug.Log("Nivel Completado: " + currentLevelData.levelName);

            LoadLevel(currentLevelIndex + 1);
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        if (prefab == null) return;

        float randomX = Random.Range(-xSpawnLimit, xSpawnLimit);
        Vector3 spawnPos = new Vector3(randomX, 6f, 0);

        GameObject newEnemyObj = Instantiate(prefab, spawnPos, prefab.transform.rotation);

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
}