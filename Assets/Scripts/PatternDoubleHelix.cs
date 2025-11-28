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
            // ESPIRAL 1: Gira a la derecha (+)
            float angleA = angleClockwise + (i * angleSegment);
            Bullet b1 = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b1.direction = GetDirectionFromAngle(angleA);
            b1.SetColor(patternColor); // Usa el color del enemigo

            // ESPIRAL 2: Gira a la izquierda (-)
            // Truco visual: Le sumamos offset (angleSegment/2) para que no salgan pegadas a la primera
            float angleB = angleCounterClockwise + (i * angleSegment) + (angleSegment / 2);
            Bullet b2 = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b2.direction = GetDirectionFromAngle(angleB);
            

            // Si el patrón es rojo, la inversa será azul.
            if (patternColor == BulletColor.Red) b2.SetColor(BulletColor.Blue);
            else if (patternColor == BulletColor.Blue) b2.SetColor(BulletColor.Red);
            else b2.SetColor(patternColor);
        }

        angleClockwise += rotationStep;
        angleCounterClockwise -= rotationStep;
    }
}