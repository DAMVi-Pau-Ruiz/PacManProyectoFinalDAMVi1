using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_Puntuaje : MonoBehaviour
{
    private int puntosTotales = 0;
    public void addPuntos(int puntosConseguidos)
    {
        puntosTotales += puntosConseguidos;
        Debug.Log(puntosTotales);
    }
}
