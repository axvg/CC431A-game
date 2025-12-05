using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [Header("config")]
    public Bullet bulletPrefab;
    public int defaultCapacity = 2000;
    public int maxCapacity = 10000;

    private IObjectPool<Bullet> pool;

    void Awake()
    {
        Instance = this;
        pool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolObject,
            false,
            defaultCapacity,
            maxCapacity
        );
    }

    // pool functions
    private Bullet CreateBullet()
    {
        Bullet b = Instantiate(bulletPrefab);
        b.SetPool(pool);
        return b;
    }

    private void OnTakeFromPool(Bullet b)
    {
        b.gameObject.SetActive(true);
    }

    private void OnReturnToPool(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet b)
    {
        Destroy(b.gameObject);
    }

    // take your bullets
    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        Bullet b = pool.Get();
        b.transform.position = position;
        b.transform.rotation = rotation;
        return b;
    }
}
