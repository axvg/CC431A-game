using UnityEngine;

public class PatternGalaxy: BasePattern
{
    [Header("galaxy")]
    public int arms = 4;
    public int bulletsPerArm = 5;
    public float twistFactor = 15f;

    private float currentRotation = 0f;

    public override void Trigger(Transform spawner)
    {
        for (int i = 0; i < arms; i++)
        {
            float armBaseAngle = (360f / arms) * i;

            for (int j = 0; j < bulletsPerArm; j++)
            {
                float offset = j * twistFactor; 
                float angle = armBaseAngle + currentRotation + offset;

                Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
                b.direction = GetDirectionFromAngle(angle);

                b.speed = 5f + (j * 1f); 
                b.SetColor(patternColor);
            }
        }
        currentRotation += 10f;
    }
}
