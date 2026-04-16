using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PacMan_Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask capaPared;

    private Vector2 desiredDirection;
    private Vector2 currentDirection = Vector2.right;

    private Rigidbody2D rb;
    private Animator animator;

    private bool estaNodo = false;
    private bool giraNodoFlag = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();

        if (moveInput.x != 0 && moveInput.y != 0)  moveInput.y = 0; 
        if (moveInput != Vector2.zero)  desiredDirection = moveInput; 

    }

    private void FixedUpdate()
    {
        Move();
        TryChangeDirection();

    }

    void Move() 
    { 
        rb.MovePosition(rb.position + currentDirection * speed * Time.fixedDeltaTime);

        animator.SetBool("isMoving", true);
    }

    void TryChangeDirection()
    {
        if (!IsAtCenterOfTile() || !estaNodo || giraNodoFlag) return;

        if (CanMove(desiredDirection)) currentDirection = desiredDirection;

        giraNodoFlag = true;

        RotatePacMan();
    }

    bool IsAtCenterOfTile()
    {
        Vector3 pos = transform.position;

        float cx = Mathf.Floor(pos.x) + 0.5f;
        float cy = Mathf.Floor(pos.y) + 0.5f;

        return Mathf.Abs(pos.x - cx) < 0.05f && Mathf.Abs(pos.y - cy) < 0.05f;
    }

    bool CanMove(Vector2 dir)
    {
        Vector2 size = new Vector2(0.3f, 0.3f);
        float distance = 0.55f;

        return !Physics2D.BoxCast(transform.position, size, 0, dir, distance, capaPared);
    }

    void RotatePacMan()
    {

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
            /*Atan2 convierte un vector (x,y) en un ángulo en radianes
             Devuelve radianes ? los convertimos a grados con Rad2Deg*/

            transform.rotation = Quaternion.Euler(0, 0, angle); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            estaNodo = true;
            giraNodoFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            estaNodo = false;
            giraNodoFlag = false;
        }
    }


}