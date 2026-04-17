using System.Collections.Generic;
using UnityEngine;

public class PinkyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject pacman;
    [SerializeField] LayerMask capaPared;

    private Vector2 currentDirection = Vector2.left;
    private Rigidbody2D rgb;

    private bool estaNodo;
    private bool giroNodoFlag;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        TryChangeDirection();
    }

    void Move()
    {
        rgb.MovePosition(rgb.position + currentDirection * speed * Time.fixedDeltaTime);
    }

    void TryChangeDirection()
    {
        if (!IsAtCenterOfTile() || !estaNodo || giroNodoFlag)
            return;

        List<Vector2> dirs = GetAvailableDirections();
        if (dirs.Count == 0) return;

        Vector2 target = GetPinkyTargetTile();

        Vector2 bestDir = currentDirection;
        float bestScore = Mathf.Infinity;

        foreach (var dir in dirs)
        {
            Vector2 next = (Vector2)transform.position + dir;

            float score = Vector2.Distance(next, target);

            if (score < bestScore)
            {
                bestScore = score;
                bestDir = dir;
            }
        }

        currentDirection = bestDir;
        giroNodoFlag = true;
    }

    
    Vector2 GetPinkyTargetTile()
    {
        Vector2 pacPos = Snap(pacman.transform.position);
        Vector2 dir = GetPacmanDirection();

        
        if (dir == Vector2.up)
        {
            return pacPos + new Vector2(-4, 4);
        }

        return pacPos + dir * 4;
    }

    Vector2 GetPacmanDirection()
    {
        PacMan_Controller pm = pacman.GetComponent<PacMan_Controller>();
        return pm != null ? pm.getCurrentDirection() : Vector2.left;
    }

    Vector2 Snap(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    bool IsAtCenterOfTile()
    {
        Vector2 s = Snap(transform.position);
        return Vector2.Distance(transform.position, s) < 0.05f;
    }

    bool CanMove(Vector2 dir)
    {
        Vector2 origin = Snap(transform.position);
        return !Physics2D.Raycast(origin, dir, 1f, capaPared);
    }

    List<Vector2> GetAvailableDirections()
    {
        List<Vector2> dirs = new List<Vector2>();

        if (CanMove(Vector2.up)) dirs.Add(Vector2.up);
        if (CanMove(Vector2.left)) dirs.Add(Vector2.left);
        if (CanMove(Vector2.down)) dirs.Add(Vector2.down);
        if (CanMove(Vector2.right)) dirs.Add(Vector2.right);

        return dirs;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            estaNodo = true;
            giroNodoFlag = false;
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
}