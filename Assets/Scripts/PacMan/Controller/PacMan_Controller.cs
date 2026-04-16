using UnityEngine;
using UnityEngine.InputSystem;

public class PacMan_Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask capaPared;

    private Vector2 desiredDirection;
    private Vector2 currentDirection = Vector2.right;

    private Rigidbody2D rb;
    private Animator animator;

    private bool enNodo = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x != 0 && input.y != 0)
            input.y = 0;

        if (input != Vector2.zero)
            desiredDirection = input;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // ?? SOLO girar en nodo
        if (enNodo && CanMove(desiredDirection))
        {
            currentDirection = desiredDirection;
            RotatePacMan();
        }

        Vector2 nextPos = rb.position + currentDirection * speed * Time.fixedDeltaTime;

        if (!IsBlocked(currentDirection))
        {
            rb.MovePosition(nextPos);
        }

        animator.SetBool("isMoving", currentDirection != Vector2.zero);
    }

    bool IsBlocked(Vector2 dir)
    {
        return Physics2D.BoxCast(
            rb.position,
            new Vector2(0.3f, 0.3f),
            0f,
            dir,
            0.5f,
            capaPared
        );
    }

    bool CanMove(Vector2 dir)
    {
        return !IsBlocked(dir);
    }

    void RotatePacMan()
    {
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // ?? NODOS = puntos de decisión
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            enNodo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            enNodo = false;
        }
    }
}