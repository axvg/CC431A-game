using UnityEngine;
using System.Collections;

public class PatternWhip : BasePattern
{
    [Header("Látigos Curvos")]
    public int arms = 4;           // Número de látigos
    public int bulletsPerArm = 10; // Longitud del látigo
    public float angleDelay = 5f;  // Cuánto se curva el látigo entre bala y bala
    public float baseRotationSpeed = 15f; // Giro general

    private float mainRotation = 0f;

    private Vector2[] baseDirections;
    private float[] baseSpeeds;

    void Awake()
    {
        PrecalculatePattern();
    }

    void PrecalculatePattern()
    {
        int totalBullets = arms * bulletsPerArm;
        baseDirections = new Vector2[totalBullets];
        baseSpeeds = new float[totalBullets];

        float angleStep = 360f / arms;
        int index = 0;

        for (int i = 0; i < arms; i++)
        {
            float armBaseAngle = i * angleStep;
            for (int j = 0; j < bulletsPerArm; j++)
            {
                float finalAngle = armBaseAngle + (j * angleDelay);
                baseDirections[index] = GetDirectionFromAngle(finalAngle);
                baseSpeeds[index] = 5f + (j * 0.5f);
                index++;
            }
        }
    }

    public override void Trigger(Transform spawner)
    {
        StartCoroutine(SpawnRoutine(spawner));
    }

    IEnumerator SpawnRoutine(Transform spawner)
    {
        mainRotation += baseRotationSpeed;
        
        float rotRad = mainRotation * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rotRad);
        float sin = Mathf.Sin(rotRad);

        Vector3 spawnPos = spawner.position;
        BulletPoolManager pool = BulletPoolManager.Instance;

        int index = 0;
        for (int i = 0; i < arms; i++)
        {
            for (int j = 0; j < bulletsPerArm; j++)
            {
                if (spawner == null) yield break;

                Bullet b = pool.GetBullet(spawnPos, Quaternion.identity);
                
                Vector2 baseDir = baseDirections[index];
                float newX = baseDir.x * cos - baseDir.y * sin;
                float newY = baseDir.x * sin + baseDir.y * cos;

                b.direction = new Vector2(newX, newY);
                b.speed = baseSpeeds[index];
                b.SetColor(patternColor);
                
                index++;
            }
            yield return null;
        }
    }
}