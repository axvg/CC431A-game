using UnityEngine;

public class PatternWhip : BasePattern
{
    [Header("Látigos Curvos")]
    public int arms = 4;           // Número de látigos
    public int bulletsPerArm = 10; // Longitud del látigo
    public float angleDelay = 5f;  // Cuánto se curva el látigo entre bala y bala
    public float baseRotationSpeed = 15f; // Giro general

    private float mainRotation = 0f;

    public override void Trigger(Transform spawner)
    {
        float angleStep = 360f / arms;

        for (int i = 0; i < arms; i++)
        {
            float armAngle = (i * angleStep) + mainRotation;

            for (int j = 0; j < bulletsPerArm; j++)
            {
                float finalAngle = armAngle + (j * angleDelay);
                
                Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
                b.direction = GetDirectionFromAngle(finalAngle);

                b.speed = 5f + (j * 0.5f); 
                b.SetColor(patternColor);
            }
        }

        mainRotation += baseRotationSpeed;
    }
}