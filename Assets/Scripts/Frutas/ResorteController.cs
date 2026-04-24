using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResorteController : FruitController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            FindObjectOfType<FruitManager>().FrutaComida();

            Destroy(gameObject);
        }
    }
}
