using UnityEngine;

public class PatternController : MonoBehaviour
{
    public float fireRate = .5f;
    private float timer;

    public BasePattern pattern;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (pattern != null)
            {
                pattern.Trigger(transform);
            }
            timer = 0f;
        }
    }
}