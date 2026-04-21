using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Esquina : MonoBehaviour
{
    [SerializeField]
    Transform[] positionsToChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            InkyController inky = collision.GetComponent<InkyController>();
            if (inky != null)
            {
                UpdatePosition();
                return;
            }
        }
    }

    private void UpdatePosition()
    {
        Transform puntoAleatorio = positionsToChange[Random.Range(0, positionsToChange.Length)];
        transform.position = puntoAleatorio.position;
    }
}
