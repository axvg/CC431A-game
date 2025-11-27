using UnityEngine;

public class PatternSpiral : BasePattern
{
    [Header("spiral settings")]
    public int bulletArms = 2;
    public float angleStep = 10f;
    public bool reverse = false;

    private float currentRotation = 0f;

    public override void Trigger(Transform spawner)
    {
        float anglePerArm = 360f / bulletArms;

        for (int i = 0; i < bulletArms; i++)
        {
            float angle = (i * anglePerArm) + currentRotation;

            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(angle);
        }

        if (reverse)
            currentRotation -= angleStep;
        else
            currentRotation += angleStep;
    }
}
