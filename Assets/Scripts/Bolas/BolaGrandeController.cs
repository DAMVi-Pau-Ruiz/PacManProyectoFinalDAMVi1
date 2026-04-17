using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaGrandeController : MonoBehaviour
{
    [SerializeField] int puntos = 1000;

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

            BolasManager.Instance.PelletEaten();

            Destroy(gameObject);
        }
    }
}
