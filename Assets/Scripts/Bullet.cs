using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction;

    // public System.Action<Bullet> onBulletDestroyed;

    private IObjectPool<Bullet> pool;

    private float lifeTime = 0f;
    private float maxLifeTime = 5f;

    public BulletColor bc;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetColor(BulletColor newType)
    {
        bc = newType;
        if (sr != null)
        {
            if (bc == BulletColor.Red)
            {
                sr.color = Color.red;
            }
            else if (bc == BulletColor.Blue)
            {
                sr.color = Color.blue;
            }
            else if (bc == BulletColor.Green)
            {
                sr.color = Color.green;
            }
        }
    }

    private float deathTime;

    void OnEnable()
    {
        deathTime = Time.time + maxLifeTime;
    }

    public float minSpeed = 0.5f;
    public float maxSpeed = 20f;

    // Culling bounds
    private float xBound = 12f;
    private float yBound = 12f;

    void Update()
    {
        // speed = Mathf.Clamp(speed, minSpeed, maxSpeed); // Optimization: Assumed set correctly by spawner
        // transform.Translate(direction * speed * Time.deltaTime); // Optimization: direction is already normalized
        transform.position += (Vector3)(direction * speed * Time.deltaTime); // Optimization: Direct position update avoids Translate overhead
        
        // lifeTime += Time.deltaTime;

        // Check bounds
        if (Mathf.Abs(transform.position.x) > xBound || Mathf.Abs(transform.position.y) > yBound)
        {
            ReturnToPool();
            return;
        }

        if (Time.time >= deathTime)
        {
            // Desactivate();
            ReturnToPool();
        }
    }

    public void Desactivate()
    {
        // onBulletDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
    }

    public void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
