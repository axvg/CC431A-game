using UnityEngine;

public class PatternSpiral : BasePattern
{
    [Header("spiral settings")]
    public int bulletArms = 5;
    public float angleStep = 10f;
    public bool reverse = false;

    [Header("touhou")]
    public float angleStepRate = 0f; // if > 0, angleStep will wave
    private float timeAlive;

    private float currentRotation = 0f;

    public override void Trigger(Transform spawner)
    {
        float anglePerArm = 360f / bulletArms;
        timeAlive += Time.deltaTime;

        // Wave the angle step if rate > 0
        float currentAngleStep = angleStep;
        if (angleStepRate > 0)
        {
            currentAngleStep += Mathf.Sin(timeAlive * angleStepRate) * 5f; 
        }

        for (int i = 0; i < bulletArms; i++)
        {
            float angle = (i * anglePerArm) + currentRotation;

            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(angle);
            b.SetColor(patternColor);
        }

        if (reverse)
            currentRotation -= currentAngleStep;
        else
            currentRotation += currentAngleStep;
    }
}
