using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public float speed = 5f;
    public Key inputUp = Key.W;
    public Key inputDown = Key.S;
    public Key inputLeft = Key.A;
    public Key inputRight = Key.D;

    private Vector2 direction = Vector2.down;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Keyboard.current[inputUp].isPressed)
        {
            SetDirection(Vector2.up);
        }
        else if (Keyboard.current[inputDown].isPressed)
        {
            SetDirection(Vector2.down);
        }   
        else if (Keyboard.current[inputLeft].isPressed)
        {
            SetDirection(Vector2.left);
        }
        else if (Keyboard.current[inputRight].isPressed)
        {
            SetDirection(Vector2.right);
        }
        else
        {
            SetDirection(Vector2.zero);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
}
