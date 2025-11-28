using UnityEngine;
using System.Collections.Generic;

[System.Serializable] 
public class Wave
{
    public string name;
    // public BasePattern pattern;
    public GameObject enemyPrefab;
    public float duration;
    public float spawnRate;
}

public class PatternController : MonoBehaviour
{
    [Header("level settings")]
    public List<Wave> levelWaves;
    public bool loopLevel = true;

    private int currentWaveIndex = 0;
    private float waveTimer = 0f;
    private float fireTimer = 0f;

    public float fireRate = .5f;
    private float timer;

    public float xSpawnLimit = 8f;
    private float spawnTimer;
    // public BasePattern pattern;

    void Start()
    {
        currentWaveIndex = 0;
        waveTimer = 0f;
        fireTimer = 0f;
    }

    void Update()
    {
        if (levelWaves.Count == 0)
            return;

        Wave currentWave = levelWaves[currentWaveIndex];

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.spawnRate)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            spawnTimer = 0f;
        }

        waveTimer += Time.deltaTime;
        if (waveTimer >= currentWave.duration)
        {
            NextWave();
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        if (prefab == null) return;

        float randomX = Random.Range(-xSpawnLimit, xSpawnLimit);
        Vector3 spawnPos = new Vector3(randomX, 6f, 0);

        // Instantiate(prefab, spawnPos, Quaternion.identity); // enemy in space
        // Instantiate(prefab, spawnPos, prefab.transform.rotation); // enemy facing down
        Instantiate(prefab, spawnPos, Quaternion.Euler(0, 0, 180));
    }

    void NextWave()
    {
        waveTimer = 0f;
        currentWaveIndex++;

        if (currentWaveIndex >= levelWaves.Count)
        {
            if (loopLevel)
            {
                currentWaveIndex = 0;
                Debug.Log("-> restarted level");
            }
            else
            {
                currentWaveIndex = levelWaves.Count - 1;
                Debug.Log("-> end of level");
            }
        } else
        {
            Debug.Log("-> start wave: " + levelWaves[currentWaveIndex].name);
        }
    }
}