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
