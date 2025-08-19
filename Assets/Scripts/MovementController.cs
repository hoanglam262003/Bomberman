using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;

    public Key inputUp = Key.W;
    public Key inputDown = Key.S;
    public Key inputLeft = Key.A;
    public Key inputRight = Key.D;

    public AnimatedSprite spriteRendererUp;
    public AnimatedSprite spriteRendererDown;
    public AnimatedSprite spriteRendererLeft;
    public AnimatedSprite spriteRendererRight;
    public AnimatedSprite spriteRendererDeath;
    private AnimatedSprite activeSpriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
        if (Keyboard.current[inputUp].isPressed)
        {
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Keyboard.current[inputDown].isPressed)
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }   
        else if (Keyboard.current[inputLeft].isPressed)
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Keyboard.current[inputRight].isPressed)
        {
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSprite spriteRenderer)
    {
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindAnyObjectByType<GameManager>().CheckWinState();
    }
}
