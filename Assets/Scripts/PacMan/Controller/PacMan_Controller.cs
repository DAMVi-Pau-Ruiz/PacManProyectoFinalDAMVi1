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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // evitar diagonales
        if (input.x != 0 && input.y != 0)
            input.y = 0;

        if (input != Vector2.zero)
            desiredDirection = input;
    }

    private void FixedUpdate()
    {
        TryChangeDirection();
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + currentDirection * speed * Time.fixedDeltaTime);

        animator.SetBool("isMoving", currentDirection != Vector2.zero);
    }

    void TryChangeDirection()
    {
        if (desiredDirection == Vector2.zero)
            return;

        // giro inmediato si no hay pared
        if (!IsBlocked(desiredDirection))
        {
            currentDirection = desiredDirection;
            RotatePacMan();
        }
    }

    bool IsBlocked(Vector2 dir)
    {
        return Physics2D.Raycast(
            rb.position,
            dir,
            0.6f,
            capaPared
        );
    }

    void RotatePacMan()
    {
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}