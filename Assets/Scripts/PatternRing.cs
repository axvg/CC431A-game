using UnityEngine;

public class PatternRing: BasePattern
{
    [Header("ring pattern settings")]
    public int bulletCount = 12;

    public override void Trigger(Transform spawner)
    {
        float angleStep = 360f / bulletCount;
        float currentAngle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);

            Vector2 shootDirection = GetDirectionFromAngle(currentAngle);
            b.direction = shootDirection;
            currentAngle += angleStep;
        }
    }
    
}
