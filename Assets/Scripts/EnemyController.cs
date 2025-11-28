using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // [Header("movement")]
    // public float speed = 3f;
    // public float lifeY = -10f;

    [Header("curve movement")]
    public float speed = 3f;
    public AnimationCurve horizontalCurve;
    public float curveWidth = 2f;

    [Header("config")]
    public float lifeY = -10f;
    private float fireRate = 1.5f;

    public float timer;
    public float age;
    private Vector3 startPos;


    // [Header("shoot")]
    // public float fireRate = 1.5f;
    // private float timer;
    private SpriteRenderer sr;
    private BasePattern myPattern;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        myPattern = GetComponent<BasePattern>();
    }

    public void SetEnemyColor(BulletColor color)
    {
        if (sr != null)
        {
            switch (color)
            {
                case BulletColor.Red:
                    sr.color = Color.red;
                    break;
                case BulletColor.Blue:
                    sr.color = Color.blue;
                    break;
                case BulletColor.Green:
                    sr.color = Color.green;
                    break;
                default:
                    sr.color = Color.white;
                    break;
            }
        }

        if (myPattern != null)
        {
            Debug.Log("Setting pattern from: " + myPattern.patternColor + " to: " + color);
            myPattern.patternColor = color;
            Debug.Log("Pattern color set to: " + myPattern.patternColor);
        }
    }

    void Start()
    {
        timer = fireRate * .8f; // delay first shot
        startPos = transform.position;
    }

    void Update()
    {
        age += Time.deltaTime;
        float newY = startPos.y - (speed * age);
        float curveX = horizontalCurve.Evaluate(age);
        float newX = startPos.x + (curveX * curveWidth);

        transform.position = new Vector3(newX, newY, 0);

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (myPattern != null)
            {
                myPattern.Trigger(this.transform);
            }
            timer = 0f;
        }
        if (transform.position.y <= lifeY)
        {
            Destroy(gameObject);
        }
    }
}
