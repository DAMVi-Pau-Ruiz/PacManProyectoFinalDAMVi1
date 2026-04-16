using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PacMan_Controller : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 moveInput;
    private Vector2 currentDirection;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentDirection = Vector2.right;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();


        if (moveInput.x != 0 && moveInput.y != 0)
        {
            moveInput.x = 0;
        }

    }

    void RotatePacMan()
    {
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            /*Atan2 convierte un vector (x,y) en un ángulo en radianes
             Devuelve radianes ? los convertimos a grados con Rad2Deg*/

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = currentDirection * speed;

        animator.SetBool("isMoving", true);

        if (moveInput != Vector2.zero)
        {
            currentDirection = moveInput;
        }

        RotatePacMan();

    }
}