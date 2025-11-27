using UnityEngine;
using System.Collections.Generic;

[System.Serializable] 
public class Wave
{
    public string name;
    public BasePattern pattern;
    public float duration;
    public float fireRate;
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
        fireTimer += Time.deltaTime;

        if (fireRate > 0f && fireTimer >= currentWave.fireRate)
        {
            if (currentWave.pattern != null)
            {
                currentWave.pattern.Trigger(transform);
            }
            fireTimer = 0f;
        }

        waveTimer += Time.deltaTime;
        if (waveTimer >= currentWave.duration)
        {
            NextWave();
        }
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