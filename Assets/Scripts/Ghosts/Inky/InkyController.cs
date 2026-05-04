using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class InkyController : GhostsController, IInvertibleDirection
{
    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask capaPared;

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
    private bool giroNodoFlag = false;
    private GameObject target;
    private Vector2 ultimaCasillaSegura;
<<<<<<< HEAD
=======
    private PacMan_Controller pac;
>>>>>>> fc58e51a4e400820d81d7e3a47184122c131305b



    void Start()
    {

        rgb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        float cx = Mathf.Floor(transform.position.x) + 0.5f;
        float cy = Mathf.Floor(transform.position.y) + 0.5f;
        transform.position = new Vector2(cx, cy);

        BuscarEsquina();
    }

    private void FixedUpdate()
    {
        pac = GameManager.instance.GetPacmanScript();
        target = GameObject.FindGameObjectWithTag("Esquina");

        UpdateUltimaCasillaSegura();

        if (target == null)
        {
            transform.position = ultimaCasillaSegura;
            rgb.velocity = Vector2.zero;
            return;
        }
        Move();
        UpdateSprite();
        TryChangeDirection();
    }

    private void Move()
    {
        rgb.MovePosition(rgb.position + currentDirection * speed * Time.fixedDeltaTime);
    }

    private void TryChangeDirection()
    {
        if (target == null)
        {
            return;
        }

        if (pac != null && pac.state == PacMan_Controller.PacManState.DIABLO)
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

            Vector2 targetPos = target.transform.position;
            float bestDist = Mathf.Infinity;
            Vector2 bestDir = currentDirection;

            foreach (var dir in dirs)
            {
                Vector2 nextPos = (Vector2)transform.position + dir;
                float dist = Vector2.Distance(nextPos, targetPos);

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

    private Vector2 GetRandomDirection(List<Vector2> dirs)
    {
        int randomIndex = Random.Range(0, dirs.Count);
        return dirs[randomIndex];
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
        if (collision.CompareTag("Esquina"))
        {
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

    private void BuscarEsquina()
    {
        target = GameObject.FindGameObjectWithTag("Esquina");
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
        if (pac != null && pac.state == PacMan_Controller.PacManState.DIABLO)
        {
            sr.sprite = scaredSprite;
        }
        else
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
    }

    public void InvertDirection()
    {
        currentDirection = -currentDirection;
    }
}