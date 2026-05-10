using MongoDB.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static PacMan_Controller;

public class PacMan_Controller : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask capaPared;

    [SerializeField]
    private AudioClip deadSound;

    [SerializeField]
    private AudioSource moveSound;

    private Vector2 desiredDirection;
    private Vector2 currentDirection = Vector2.right;

    private bool estaNodo = false;
    private bool giroNodoFlag = false;
    private bool isDead = false;

    private Animator animator;
    private Collider2D colider;

    private float baseSpeed;
    private int invertMove = 1;

    private Coroutine speedBoostCoroutine;
    private Coroutine invincibleCoroutine;
    private Coroutine invertCoroutine;

    public enum PacManState
    {
        NORMAL, DIABLO, INVENCIBLE
    }

    public PacManState state = PacManState.NORMAL;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
        baseSpeed = speed;
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x != 0 && input.y != 0)
        {
            input.y = 0;
        }
        if (input != Vector2.zero)
        {
            desiredDirection = input * invertMove;
        }
    }

    private void Update()
    {
        TryToChangeDirection();
        Move();
    }

    private void Move()
    {
        if (!isDead)
        {
            Vector2 pos = transform.position;
            if (IsBlocked(currentDirection))
            {
                currentDirection = Vector2.zero;
            }

            float cx = Mathf.Floor(pos.x) + 0.5f;
            float cy = Mathf.Floor(pos.y) + 0.5f;

            float alignSpeed = 20;

            if (currentDirection == Vector2.right || currentDirection == Vector2.left)
            {
                pos.y = Mathf.Lerp(pos.y, cy, alignSpeed * Time.deltaTime);
            }
            else if (currentDirection == Vector2.up || currentDirection == Vector2.down)
            {
                pos.x = Mathf.Lerp(pos.x, cx, alignSpeed * Time.deltaTime);
            }

            pos += currentDirection * speed * Time.deltaTime;

            transform.position = pos;

            bool isMoving = currentDirection != Vector2.zero;

            animator.SetBool("isMoving", isMoving);


            if (isMoving && !isDead)
            {
                if (!moveSound.isPlaying)
                    moveSound.Play();
            }
            else
            {
                if (moveSound.isPlaying)
                    moveSound.Stop();
            }
        }

    }

    private void TryToChangeDirection()
    {
        if (currentDirection == Vector2.zero && desiredDirection != Vector2.zero)
        {
            if (!IsBlocked(desiredDirection))
            {
                currentDirection = desiredDirection;
                RotatePacMan();
                return;
            }
        }

        if (desiredDirection == -currentDirection && desiredDirection != Vector2.zero)
        {
            currentDirection = desiredDirection;
            RotatePacMan();
            return;
        }

        if (!estaNodo || giroNodoFlag)
        {
            return;
        }

        if (desiredDirection != Vector2.zero && !IsBlocked(desiredDirection))
        {
            currentDirection = desiredDirection;
            RotatePacMan();
            giroNodoFlag = true;
        }
    }

    private bool IsBlocked(Vector2 dir)
    {
        Vector2 size = new Vector2(0.25f, 0.25f);
        float distance = 0.5f;
        return Physics2D.BoxCast(transform.position, size, 0, dir, distance, capaPared);
    }

    private void RotatePacMan()
    {
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isDead)
        {
            if (collision.CompareTag("Nodo"))
            {
                estaNodo = true;
                giroNodoFlag = false;
            }
            else if (collision.CompareTag("Enemy"))
            {
                switch (state)
                {
                    case PacManState.NORMAL:
                        Dead();
                        break;
                    case PacManState.DIABLO:
                        EatGhost(collision);
                        break;
                    case PacManState.INVENCIBLE:
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            estaNodo = false;
            giroNodoFlag = false;
        }
    }


    private void Dead()
    {
        isDead = true;

        if (moveSound.isPlaying)
            moveSound.Stop();

        AudioManager.Instance.PlaySFX(deadSound);

        colider.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        animator.SetBool("isDead", true);

        StartCoroutine(DeathSequence());

    }

    private void EatGhost(Collider2D collision)
    {
        string ghostName = collision.gameObject.name;

        Destroy(collision.gameObject);
        FindObjectOfType<GhostsSpawner>().MarkGhostAsEaten(ghostName);
        gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(collision.GetComponent<GhostsController>().GetPuntos());
        GameManager.instance.addGhostEaten();

    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(0.9f);
        GameManager.instance.PacmanDied();
        Destroy(gameObject);
    }

    public Vector2 getCurrentDirection()
    {
        return currentDirection;
    }

    public void ActivarSpeedBoost(float multiplicador, float duracion)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoostRutina(multiplicador, duracion));
    }

    private IEnumerator SpeedBoostRutina(float multiplicador, float duracion)
    {
        speed = baseSpeed * multiplicador;

        yield return new WaitForSeconds(duracion);

        speed = baseSpeed;
    }

    public void ActivarInvincible(float duracion)
    {
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }
        invincibleCoroutine = StartCoroutine(InvincibleRutina(duracion));
    }

    private IEnumerator InvincibleRutina(float duracion)
    {
        state = PacManState.INVENCIBLE;

        yield return new WaitForSeconds(duracion);

        state = PacManState.NORMAL;

    }

    public void ActivarInvertido(float duracion)
    {
        if (invertCoroutine != null)
            StopCoroutine(invertCoroutine);

        invertCoroutine = StartCoroutine(InvertidoRutina(duracion));
    }

    private IEnumerator InvertidoRutina(float duracion)
    {
        invertMove = -1;
        animator.SetBool("isInvert", true);

        yield return new WaitForSeconds(duracion);

        animator.SetBool("isInvert", false);
        invertMove = 1;
    }
}