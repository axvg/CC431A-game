using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 8f;
    public float focusSpeed = 3f;
    public float slowMotionFactor = 0.5f;
    public Vector2 boundary = new Vector2(8.5f, 4.5f);

    public BulletColor playerColor;
    private SpriteRenderer sr;

    [Header("hitbox")]
    public GameObject hitboxVisual;
    private SpriteRenderer hitboxSr;

    private List<BulletColor> availableColors = new List<BulletColor>();
    private int colorIndex = 0;

    private bool canFocus = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // UpdateColorVisuals();
        if (hitboxVisual != null)
        {
            hitboxSr = hitboxVisual.GetComponent<SpriteRenderer>();
            hitboxVisual.SetActive(false);
        }
    }

    public void ConfigureAbilities(List<BulletColor> newColors, bool enableFocus) 
    {
        availableColors = new List<BulletColor>(newColors);
        if (availableColors.Count > 0)
        {
            colorIndex = 0;
            SetColor(availableColors[0]);
        }
        
        canFocus = enableFocus;
    }

    void SwapColor()
    {
        if (playerColor == BulletColor.Cyan)
        {
            playerColor = BulletColor.Magenta;
        }
        else
        {
            playerColor = BulletColor.Cyan;
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
            if (playerColor == BulletColor.Cyan)
            {
                sr.color = Color.cyan;
            }
            else if (playerColor == BulletColor.Magenta)
            {
                sr.color = Color.magenta;
            }
        }
    }

    void Update()
    {
        float currentSpeed = speed;

        if (canFocus && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (Time.timeScale != slowMotionFactor)
            {
                Time.timeScale = slowMotionFactor;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                if(hitboxSr != null) hitboxSr.color = Color.white;
                
                Color transparentColor = sr.color;
                transparentColor.a = 0.1f;
                sr.color = transparentColor;
            }
        }
        else
        {
            if (Time.timeScale != 1f)
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f;
                if (hitboxVisual != null) hitboxVisual.SetActive(false);

                Color opaqueColor = sr.color;
                opaqueColor.a = 1f; 
                sr.color = opaqueColor;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        // transform.Translate(moveDir * currentSpeed * Time.unscaledDeltaTime);
        transform.position += (Vector3)(moveDir * currentSpeed * Time.unscaledDeltaTime);

    
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
            case BulletColor.Cyan: sr.color = Color.cyan; break;
            case BulletColor.Magenta: sr.color = Color.magenta; break;
            case BulletColor.Yellow: sr.color = Color.yellow; break;
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
                Debug.Log("damage");
                
                if (ScoreManager.Instance != null) 
                {
                    ScoreManager.Instance.LoseLife();
                }
                bullet.ReturnToPool();
            }
        }
    }

    // helper
    Color GetColorFromEnum(BulletColor c)
    {
        switch (c)
        {
            case BulletColor.Cyan: return Color.cyan;
            case BulletColor.Magenta: return Color.magenta;
            case BulletColor.Yellow: return Color.yellow;
            default: return Color.white;
        }
    }
}
