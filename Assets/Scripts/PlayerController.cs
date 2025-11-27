using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 10f;
    public Vector2 boundary = new Vector2(8.5f, 4.5f);

    public BulletColor playerColor = BulletColor.Red;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColorVisuals();
    }

    void SwapColor()
    {
        if (playerColor == BulletColor.Red)
        {
            playerColor = BulletColor.Blue;
        }
        else
        {
            playerColor = BulletColor.Red;
        }

        UpdateColorVisuals();
    }

    void UpdateColorVisuals()
    {
        if (sr != null)
        {
            if (playerColor == BulletColor.Red)
            {
                sr.color = Color.red;
            }
            else if (playerColor == BulletColor.Blue)
            {
                sr.color = Color.blue;
            }
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        transform.Translate(moveDir * speed * Time.deltaTime);

    
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundary.x, boundary.x);
        pos.y = Mathf.Clamp(pos.y, -boundary.y, boundary.y);
        transform.position = pos;
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapColor();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Bullet b = other.GetComponent<Bullet>();
        if (b != null)
        {
            if (b.bc != playerColor)
            {
                Debug.Log("Game Over");
            }
            else
            {
                Debug.Log("safe");
                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.AddScore(1);
                }
                b.ReturnToPool();
            }

            // b.Desactivate();
        }
    }
}
