using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerezaController : FruitController
{
    [SerializeField]
    int multi = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            collision.GetComponent<PacMan_Puntuaje>().ActivarMulti(multi, duration);

            FindObjectOfType<FruitManager>().FrutaComida();

            GameManager.instance.cerezaComida = true;

            Destroy(gameObject);
        }
    }
}
