using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string name;
    public GameObject[] enemyOptions;
    public float spawnRate;
    public float duration;

    public float[] spawnXPositions;
}

public enum LevelColorMode
{
    Constant,
    Random
}

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "Nivel 1";
    public List<Wave> waves;

    [Header("Level info")]
    [TextArea]
    public string levelInfo;

    [Header("Level color settings")]
    public LevelColorMode colorMode = LevelColorMode.Constant;

    [Header("player config")]
    public List<BulletColor> playerColors;
    public bool allowFocus;

    [Header("tutorial msg")]
    [TextArea]
    public string startMessage;

    [Header("audios")]
    public AudioClip levelMusic;
    public AudioClip startMusic;

    public BulletColor constantColor;
    public BulletColor[] randomColors;

    void OnValidate()
    {
        float totalSeconds = 0f;
        int totalEnemiesMin = 0;
        foreach (var wave in waves)
        {
            totalSeconds += wave.duration;

            if (wave.spawnRate > 0)
            {
                totalEnemiesMin += Mathf.CeilToInt(wave.duration / wave.spawnRate);
            }
        }

        // format
        int minutes = Mathf.FloorToInt(totalSeconds / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);

        levelInfo = $"duracion total: {totalSeconds} seg ({minutes}:{seconds:00})\n" +
                    $"enemies aprox: {totalEnemiesMin}";
    }
}
