using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BolaPetitController : MonoBehaviour
{
    [SerializeField]
    int puntos = 10;

    private void Start()
    {
        BolasManager.Instance.RegisterPellet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PacMan_Puntuaje>().addPuntos(puntos);

            BolasManager.Instance.PelletEaten();

            Destroy(gameObject);
        }
    }
}
