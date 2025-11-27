using UnityEngine;

public class PatternMixed : BasePattern
{
    [Header("mixed pattern settings")]
    public int totalArms = 4;
    public float rotationStep = 10f;
    
    private float currentRotation = 0f;

    public override void Trigger(Transform spawner)
    {
        float anglePerArm = 360f / totalArms;

        for (int i = 0; i < totalArms; i++)
        {
            float angle = (i * anglePerArm) + currentRotation;
            
            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(angle);

            if (i % 2 == 0)
            {
                b.SetColor(BulletColor.Red);
            }
            else
            {
                b.SetColor(BulletColor.Blue);
            }
        }

        currentRotation += rotationStep;
    }
}
