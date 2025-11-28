using UnityEngine;

public class PatternAccSpiral: BasePattern
{
    [Header("vortice")]
    public float startAngleStep = 10f;
    public float acceleration = 0.5f;
    public float maxStep = 45f;
    
    private float currentAngle = 0f;
    private float currentStep;

    void OnEnable()
    {
        currentStep = startAngleStep;
    }

    public override void Trigger(Transform spawner)
    {
        Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
        b.direction = GetDirectionFromAngle(currentAngle);
        b.SetColor(patternColor);
        currentAngle += currentStep;
        currentStep = startAngleStep + Mathf.PingPong(Time.time * 5f, maxStep);
    } 
}
