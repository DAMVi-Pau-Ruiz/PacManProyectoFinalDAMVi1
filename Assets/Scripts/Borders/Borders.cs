using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IInvertibleDirection ghost = collision.GetComponent<IInvertibleDirection>();
            if (ghost != null)
            {
                ghost.InvertDirection();
            }
        }
    }
}
