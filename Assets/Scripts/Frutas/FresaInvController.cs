using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FresaInvController : FruitController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            collision.gameObject.GetComponent<Controller_PacMan>().ActivarInvertido(duration);

            FindObjectOfType<FruitManager>().FrutaComida();

            Destroy(gameObject);
        }
    }
}
