using UnityEngine;

public class PatternDoubleHelix : BasePattern
{
    [Header("Doble Hélice")]
    public int bulletsPerWave = 10;
    public float rotationStep = 12f;
    
    private float angleClockwise = 0f;
    private float angleCounterClockwise = 0f;

    public override void Trigger(Transform spawner)
    {
        float angleSegment = 360f / bulletsPerWave;

        for (int i = 0; i < bulletsPerWave; i++)
        {
            float angleA = angleClockwise + (i * angleSegment);
            Bullet b1 = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b1.direction = GetDirectionFromAngle(angleA);
            b1.SetColor(patternColor); // Usa el color del enemigo

            float angleB = angleCounterClockwise + (i * angleSegment) + (angleSegment / 2);
            Bullet b2 = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b2.direction = GetDirectionFromAngle(angleB);
            

            if (patternColor == BulletColor.Cyan) b2.SetColor(BulletColor.Magenta);
            else if (patternColor == BulletColor.Magenta) b2.SetColor(BulletColor.Cyan);
            else b2.SetColor(patternColor);
        }

        angleClockwise += rotationStep;
        angleCounterClockwise -= rotationStep;
    }
}