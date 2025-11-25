using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    // public Bullet bulletPrefab;
    public float fireRate = .05f;

    public float timer;
    public float angle = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        // Bullet b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet b = BulletPoolManager.Instance.GetBullet(transform.position, Quaternion.identity);
        float rad = angle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        b.direction = dir;
        angle += 13f;
    }
}
