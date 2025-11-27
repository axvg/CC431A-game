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
        }
    }

    void OnEnable()
    {
        lifeTime = 0;
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime)
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
