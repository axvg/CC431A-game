using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string name;
    public GameObject[] enemyOptions;
    public float spawnRate;
    public float duration;
}

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "Nivel 1";
    public List<Wave> waves;
}
