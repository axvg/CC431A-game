using UnityEngine;

public class PatternPhyllotaxis: BasePattern
{
    [Header("config golden")]
    public int bulletAmount = 20;
    public float spread = 0.5f;
    
    private float goldenAngle = 137.508f; 
    private float currentStep = 0;

    public override void Trigger(Transform spawner)
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            // a = n * 137.5
            float angle = currentStep * goldenAngle;
            
            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(angle);
            b.SetColor(patternColor);
            
            currentStep++;
        }
    }
}
