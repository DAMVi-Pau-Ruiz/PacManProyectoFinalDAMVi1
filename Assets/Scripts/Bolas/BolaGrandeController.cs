using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaGrandeController : MonoBehaviour
{
    [SerializeField]
    int puntos = 50;

    [SerializeField]
    float duracionModoDiablo = 8;

    private void Start()
    {
        BolasManager.Instance.RegisterPellet();
        transform.localScale *= 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            GameManager.instance.ModoDiabloActivado(duracionModoDiablo);

            BolasManager.Instance.PelletEaten();

            Destroy(gameObject);
        }
    }
}
