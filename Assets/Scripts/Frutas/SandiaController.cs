using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandiaController : FruitController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            GameManager.instance.AddVida();

            FindObjectOfType<FruitManager>().FrutaComida();

            GameManager.instance.sandiaComida = true;

            Destroy(gameObject);
        }
    }
}
