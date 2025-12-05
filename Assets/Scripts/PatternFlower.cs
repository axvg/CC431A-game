using UnityEngine;

public class FlowerPattern: BasePattern
{
    [Header("math")]
    public int petals = 7;
    public int bulletCount = 80;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;

    [Header("touhou")]
    public int layers = 3;
    public float layerSpeedDiff = 1f;
    public float rotationSpeed = 30f;
    private float currentRotation = 0f;

    private Vector2[] baseDirections;
    private float[] petalSpeeds;

    void Awake()
    {
        PrecalculatePattern();
    }

    void PrecalculatePattern()
    {
        baseDirections = new Vector2[bulletCount];
        petalSpeeds = new float[bulletCount];
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = i * angleStep;
            baseDirections[i] = GetDirectionFromAngle(currentAngle);

            // Pre-calculate speed based on relative angle (petal shape)
            float rad = currentAngle * Mathf.Deg2Rad;
            float petalFactor = Mathf.Abs(Mathf.Cos(petals * rad * 0.5f));
            petalSpeeds[i] = Mathf.Lerp(minSpeed, maxSpeed, petalFactor);
        }
    }

    public override void Trigger(Transform spawner)
    {
        currentRotation += rotationSpeed * Time.deltaTime;
        
        // Calculate rotation once per frame
        float rotRad = currentRotation * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rotRad);
        float sin = Mathf.Sin(rotRad);

        Vector3 spawnPos = spawner.position;
        BulletPoolManager pool = BulletPoolManager.Instance;

        for (int l = 0; l < layers; l++)
        {
            float layerSpeedMod = l * layerSpeedDiff;

            for (int i = 0; i < bulletCount; i++)
            {
                Bullet b = pool.GetBullet(spawnPos, Quaternion.identity);
                
                // Rotate pre-calculated direction
                Vector2 baseDir = baseDirections[i];
                float newX = baseDir.x * cos - baseDir.y * sin;
                float newY = baseDir.x * sin + baseDir.y * cos;
                
                b.direction = new Vector2(newX, newY);
                b.speed = petalSpeeds[i] + layerSpeedMod;
                b.SetColor(patternColor);
            }
        }
    }
}
