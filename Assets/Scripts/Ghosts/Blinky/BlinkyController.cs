using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BlinkyController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask capaPared;

    private Vector2 currentDirection = Vector2.left;
    private Rigidbody2D rgb;
    private bool estaNodo = false;
    private bool giroNodoFlag = false;
    private bool giroIzquierda = true;
    private bool giroDerecha = true;
    private GameObject playerObj;
    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        BuscarPlayer();

        //ransform.position = new Vector3(
        //Mathf.Round(transform.position.x),
        //Mathf.Round(transform.position.y),
        //0
        //);
    }

    private void FixedUpdate()
    {
        if (playerObj == null)
        {
            BuscarPlayer();
        }

        Move();
        TryChangeDirection();
    }

    void Move()
    {
        rgb.MovePosition(rgb.position + currentDirection * speed * Time.fixedDeltaTime);
    }

    void TryChangeDirection()
    {
        if (IsAtCenterOfTile() && estaNodo && !giroNodoFlag)
        {
            Debug.Log("Giro");
            List<Vector2> dirs = GetAvailableDirection();

            Vector2 target = playerObj.transform.position;
            float bestDist = Mathf.Infinity;
            Vector2 bestDir = currentDirection;

            foreach (var dir in dirs)
            {
                Vector2 nextPos = (Vector2)transform.position + dir;
                float dist = Vector2.Distance(nextPos, target);

                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestDir = dir;
                }
            }
            currentDirection = bestDir;
            giroNodoFlag = true;
        }
    }

    bool IsAtCenterOfTile()
    {
        Vector3 pos = transform.position;

        float cx = Mathf.Floor(pos.x) + 0.5f;
        float cy = Mathf.Floor(pos.y) + 0.5f;

        return Mathf.Abs(pos.x - cx) < 0.05f &&
               Mathf.Abs(pos.y - cy) < 0.05f;
    }

    bool CanMove(Vector2 dir)
    {
        Vector2 size = new Vector2(0.3f, 0.3f);
        float distance = 0.55f;
        return !Physics2D.BoxCast(transform.position, size, 0, dir, distance, capaPared);
    }

    List<Vector2> GetAvailableDirection()
    {
        List<Vector2> dirs = new List<Vector2>();
        if (CanMove(Vector2.up)) dirs.Add(Vector2.up);
        if (giroIzquierda)
        {
            if (CanMove(Vector2.left)) dirs.Add(Vector2.left);
        }
        if (CanMove(Vector2.down)) dirs.Add(Vector2.down);
        if (giroDerecha)
        {
            if (CanMove(Vector2.right)) dirs.Add(Vector2.right);
        }

        return dirs;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            Debug.Log("Entra Colision");
            estaNodo = true;
            giroNodoFlag = false;
            return;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            Debug.Log("Sale Colision");
            estaNodo = false;
            giroNodoFlag = false;
            return;
        }
    }

    public void ActivateDirectionLR(bool izquierda)
    {
        if (izquierda)
        {
            giroIzquierda = false;
        }
        else
        {
            giroDerecha = false;
        }
    }
    public void DeactivateDirectionLR(bool izquierda)
    {
        if (izquierda)
        {
            giroIzquierda = true;
        }
        else
        {
            giroDerecha = true;
        }
    }

    void BuscarPlayer()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
}
