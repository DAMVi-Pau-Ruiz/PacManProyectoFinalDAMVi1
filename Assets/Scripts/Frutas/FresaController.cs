using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FresaController : FruitController
{
    [SerializeField]
    float multSpeed = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            collision.GetComponent<Controller_PacMan>().ActivarSpeedBoost(multSpeed, duration);

            FindObjectOfType<FruitManager>().FrutaComida();

            Destroy(gameObject);
        }
    }
}
