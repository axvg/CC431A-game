using UnityEngine;

public class PatternLotus : BasePattern
{
    [Header("Configuración del Loto")]
    public int petals = 6;
    public int bulletCount = 72;
    public float baseSpeed = 5f;
    public float petalSpeedVar = 3f;
    public float rotationSpeed = 20f;

    private float currentRotation = 0f;

    public override void Trigger(Transform spawner)
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = (i * angleStep) + currentRotation;
            
            float rad = (i * angleStep) * Mathf.Deg2Rad;
            float speedMod = Mathf.Abs(Mathf.Sin(rad * (petals / 2f))); 
            
            float finalSpeed = baseSpeed + (speedMod * petalSpeedVar);

            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(currentAngle);
            b.speed = finalSpeed;
            b.SetColor(patternColor);
        }

        currentRotation += rotationSpeed;
    }
}