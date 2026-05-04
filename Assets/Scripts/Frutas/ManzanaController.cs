using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManzanaController : FruitController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            GameManager.instance.ModoDiabloActivado(duration);

            FindObjectOfType<FruitManager>().FrutaComida();

            GameManager.instance.manzanaComida = true;

            Destroy(gameObject);
        }
    }
}
