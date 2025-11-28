using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 8f;
    public Vector2 boundary = new Vector2(8.5f, 4.5f);

    public BulletColor playerColor;
    private SpriteRenderer sr;


    private List<BulletColor> availableColors = new List<BulletColor>();
    private int colorIndex = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // UpdateColorVisuals();
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

    public void ConfigureColors(List<BulletColor> newColors)
    {
        availableColors = new List<BulletColor>(newColors);
        if (availableColors.Count > 0)
        {
            colorIndex = 0;
            SetColor(availableColors[0]);
        }
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
            if (availableColors.Count > 1)
            {
                CycleColor();
            }
        }
    }

    void CycleColor()
    {
        colorIndex++;
        if (colorIndex >= availableColors.Count)
        {
            colorIndex = 0;
        }
        SetColor(availableColors[colorIndex]);
    }

    void SetColor(BulletColor c)
    {
        playerColor = c;
        switch (c)
        {
            case BulletColor.Red: sr.color = Color.red; break;
            case BulletColor.Blue: sr.color = Color.blue; break;
            case BulletColor.Green: sr.color = Color.green; break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null)
        {
            if (bullet.bc == playerColor)
            {
                if (ScoreManager.Instance != null) ScoreManager.Instance.AddScore(100);
                bullet.ReturnToPool();
            }
            else
            {
                Debug.Log("¡Daño Recibido!");
                
                if (ScoreManager.Instance != null) 
                {
                    ScoreManager.Instance.LoseLife();
                }
                bullet.ReturnToPool();
            }
        }
    }
}
