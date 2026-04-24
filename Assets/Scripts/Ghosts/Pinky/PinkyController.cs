using System.Collections.Generic;
using UnityEngine;

public class PinkyController : GhostsController, IInvertibleDirection
{
    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask capaPared;

    [SerializeField]
    int distanciaAdelante = 4;

    [SerializeField]
    Sprite lookUp;

    [SerializeField]
    Sprite lookLeft;

    [SerializeField]
    Sprite lookDown;

    [SerializeField]
    Sprite lookRight;

    private Vector2 currentDirection = Vector2.left;
    private Rigidbody2D rgb;
    private bool estaNodo = false;
    private bool giroNodoFlag;
    private GameObject playerObj;
    private Vector2 ultimaCasillaSegura;



    void Start()
    {

        rgb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        BuscarPlayer();
    }

    void FixedUpdate()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        UpdateUltimaCasillaSegura();

        if (playerObj == null)
        {
            transform.position = ultimaCasillaSegura;
            rgb.velocity = Vector2.zero;
            return;
        }
        Move();
        UpdateSprite();
        TryChangeDirection();
    }

    void Move()
    {
        rgb.MovePosition(rgb.position + currentDirection * speed * Time.fixedDeltaTime);
    }

    void TryChangeDirection()
    {
        if (playerObj == null)
        {
            return;
        }

        // --- MODO DIABLO: movimiento aleatorio ---
        if (GameManager.instance.IsModoDiabloActivo())
        {
            if (IsAtCenterOfTile() && estaNodo && !giroNodoFlag)
            {
                List<Vector2> dirs = GetAvailableDirection();
                currentDirection = GetRandomDirection(dirs);
                giroNodoFlag = true;
            }
            return; // Evita ejecutar el comportamiento normal
        }


        if (IsAtCenterOfTile() && estaNodo && !giroNodoFlag)
        {
            List<Vector2> dirs = GetAvailableDirection();

            Vector2 target = GetPinkyTargetTile();
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

    private Vector2 GetPinkyTargetTile()
    {
        if (playerObj == null)
        {
            return transform.position;
        }

        Vector2 pacPos = Snap(playerObj.transform.position);
        Vector2 dir = GetPacmanDirection();

        // Regla especial de Pinky cuando Pac-Man mira hacia arriba
        if (dir == Vector2.up)
            return pacPos + new Vector2(-distanciaAdelante, distanciaAdelante);

        return pacPos + dir * distanciaAdelante;
    }

    private Vector2 GetPacmanDirection()
    {
        if (playerObj == null)
        {
            return Vector2.left;
        }

        PacMan_Controller pm = playerObj.GetComponent<PacMan_Controller>();
        return pm != null ? pm.getCurrentDirection() : Vector2.left;
    }

    private Vector2 Snap(Vector2 v)
    {
        float cx = Mathf.Floor(v.x) + 0.5f;
        float cy = Mathf.Floor(v.y) + 0.5f;
        return new Vector2(cx, cy);
    }

    private bool IsAtCenterOfTile()
    {
        Vector3 pos = transform.position;

        float cx = Mathf.Floor(pos.x) + 0.5f;
        float cy = Mathf.Floor(pos.y) + 0.5f;

        return Mathf.Abs(pos.x - cx) < 0.05f &&
               Mathf.Abs(pos.y - cy) < 0.05f;
    }

    private bool CanMove(Vector2 dir)
    {
        Vector2 size = new Vector2(0.3f, 0.3f);
        float distance = 0.55f;
        return !Physics2D.BoxCast(transform.position, size, 0, dir, distance, capaPared);
    }

    private List<Vector2> GetAvailableDirection()
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
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nodo"))
        {
            estaNodo = false;
            giroNodoFlag = false;
            return;
        }
    }

    private void BuscarPlayer()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void UpdateUltimaCasillaSegura()
    {
        if (IsAtCenterOfTile() && CanMove(currentDirection))
        {
            float cx = Mathf.Floor(transform.position.x) + 0.5f;
            float cy = Mathf.Floor(transform.position.y) + 0.5f;
            ultimaCasillaSegura = new Vector2(cx, cy);
        }
    }
    private void UpdateSprite()
    {
        if (!GameManager.instance.IsModoDiabloActivo())
        {
            if (currentDirection == Vector2.up)
            {
                sr.sprite = lookUp;
            }
            else if (currentDirection == Vector2.left)
            {
                sr.sprite = lookLeft;
            }
            else if (currentDirection == Vector2.down)
            {
                sr.sprite = lookDown;
            }
            else if (currentDirection == Vector2.right)
            {
                sr.sprite = lookRight;
            }
        }
        else
        {
            sr.sprite = scaredSprite;
        }
    }
    public void InvertDirection()
    {
        currentDirection = -currentDirection;
    }

    private Vector2 GetRandomDirection(List<Vector2> dirs)
    {
        int randomIndex = Random.Range(0, dirs.Count);
        return dirs[randomIndex];
    }
}
