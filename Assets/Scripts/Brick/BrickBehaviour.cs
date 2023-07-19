using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameEvent onBrickBroke;
    [SerializeField]
    private BrickScriptableObject brick;
    [SerializeField]
    private GameObject graphics;
    
    private PowerUpDropperBehaviour powerUp;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private int hitCount = 0;

    void Awake()
    {
        powerUp = GetComponent<PowerUpDropperBehaviour>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }

    void Start()
    {
        spriteRenderer.color = brick.color;

        ResetBrick();
    }

    private void ResetBrick()
    {
        boxCollider.enabled = true;
        graphics.SetActive(true);
        hitCount = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;
            if (hitCount >= brick.resistence)
            {
                onBrickBroke.Raise();
                powerUp.DropPowerUp();
                boxCollider.enabled = false;
                graphics.SetActive(false);
            }
        }
    }
}
