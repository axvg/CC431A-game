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
    private BasePattern myPattern;

    void Start()
    {
        myPattern = GetComponent<BasePattern>();
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
