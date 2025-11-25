using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction;

    public System.Action<Bullet> onBulletDestroyed;

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
            Desactivate();
        }
    }

    public void Desactivate()
    {
        onBulletDestroyed?.Invoke(this);
        gameObject.SetActive(false);
    }
}
