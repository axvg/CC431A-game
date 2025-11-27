using UnityEngine;

public abstract class BasePattern : MonoBehaviour
{
    public BulletColor patternColor = BulletColor.Red;

    public abstract void Trigger(Transform spawner);

    protected Vector2 GetDirectionFromAngle(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }
}
