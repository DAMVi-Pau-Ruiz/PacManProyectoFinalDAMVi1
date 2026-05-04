using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtupController : FruitController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            GameManager.instance.ActivarInvisibilidadFantasmas(duration);

            FindObjectOfType<FruitManager>().FrutaComida();

            GameManager.instance.atupComida = true;

            Destroy(gameObject);
        }
    }
}
