using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatanoController : FruitController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            collision.GetComponent<PacMan_Controller>().ActivarInvincible(duration);

            FindObjectOfType<FruitManager>().FrutaComida();

            Destroy(gameObject);
        }
    }
}
