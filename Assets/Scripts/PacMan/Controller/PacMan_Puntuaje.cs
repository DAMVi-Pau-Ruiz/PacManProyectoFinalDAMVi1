using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_Puntuaje : MonoBehaviour
{
    private int puntosTotales = 0;
    private Coroutine multiCoroutine;
    private int multiPuntos = 1;
    public void addPuntos(int puntosConseguidos)
    {
        puntosTotales += (puntosConseguidos * multiPuntos);
        HUDController.instance.AddScore(puntosConseguidos * multiPuntos);
    }

    public void ActivarMulti(int multi, float duracion)
    {
        if (multiCoroutine != null)
            StopCoroutine(multiCoroutine);

        multiCoroutine = StartCoroutine(MultiRutina(multi, duracion));
    }

    private IEnumerator MultiRutina(int multi, float duracion)
    {
        multiPuntos = multi;

        yield return new WaitForSeconds(duracion);

        multiPuntos = 1;
    }
}
