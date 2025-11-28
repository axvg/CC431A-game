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

    public override void Trigger(Transform spawner)
    {
        float angleStep = 360f / bulletCount;
        currentRotation += rotationSpeed * Time.deltaTime; // Rotate the whole pattern source

        for (int l = 0; l < layers; l++)
        {
            float layerSpeedMod = l * layerSpeedDiff;

            for (int i = 0; i < bulletCount; i++)
            {
                float currentAngle = (i * angleStep) + currentRotation;
                
                // polar: r = cos(k * theta)
                float rad = currentAngle * Mathf.Deg2Rad;
                float petalFactor = Mathf.Abs(Mathf.Cos(petals * rad * 0.5f)); 
                
                float finalSpeed = Mathf.Lerp(minSpeed, maxSpeed, petalFactor) + layerSpeedMod;

                Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
                b.direction = GetDirectionFromAngle(currentAngle);
                b.speed = finalSpeed;
                b.SetColor(patternColor);
            }
        }
    }
}
