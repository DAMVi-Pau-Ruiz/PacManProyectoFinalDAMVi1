using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordeSalida : MonoBehaviour
{
    [SerializeField]
    bool izquierda; //si está en la izquierda este borde, si es el de la derecha, desactivado

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BlinkyController>().ActivateDirectionLR(izquierda);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<BlinkyController>().DeactivateDirectionLR(izquierda);
    }
}
