using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("movement")]
    public float speed = 3f;
    public float lifeY = -10f;

    [Header("shoot")]
    public float fireRate = 1.5f;
    private float timer;
    private BasePattern myPattern;

    void Start()
    {
        myPattern = GetComponent<BasePattern>();
        timer = fireRate * .8f; // delay first shot
    }

    void Update()
    {
        // enemy downwards
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // shoot
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (myPattern != null)
            {
                myPattern.Trigger(this.transform);
            }
            timer = 0f;
        }
        if (transform.position.y < lifeY)
        {
            Destroy(this.gameObject);
        }
    }
}
