using UnityEngine;

public class FlowerPattern: BasePattern
{
    [Header("math")]
    public int petals = 5;
    public int bulletCount = 50;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;

    public override void Trigger(Transform spawner)
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = i * angleStep;
            
            // polar: r = cos(k * theta)
            float rad = currentAngle * Mathf.Deg2Rad;
            float petalFactor = Mathf.Abs(Mathf.Cos(petals * rad * 0.5f)); 
            
            float finalSpeed = Mathf.Lerp(minSpeed, maxSpeed, petalFactor);

            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(currentAngle);
            b.speed = finalSpeed;
            b.SetColor(patternColor);
        }
    }
}
