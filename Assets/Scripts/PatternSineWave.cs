using UnityEngine;

public class PatternSineWave: BasePattern
{
    [Header("sin")]
    public int streams = 3;
    public float angleWidth = 90f;
    public float frequency = 2f;

    private float time;

    public override void Trigger(Transform spawner)
    {
        time += Time.deltaTime;
        
        float centerAngle = Mathf.Sin(Time.time * frequency) * (angleWidth / 2);
        
        float step = 20f;
        float startAngle = centerAngle - ((streams - 1) * step / 2);

        for (int i = 0; i < streams; i++)
        {
            float angle = startAngle + (i * step);
            float finalAngle = -90f + angle; 

            Bullet b = BulletPoolManager.Instance.GetBullet(spawner.position, Quaternion.identity);
            b.direction = GetDirectionFromAngle(finalAngle);
            b.SetColor(patternColor);
        }
    }
}
