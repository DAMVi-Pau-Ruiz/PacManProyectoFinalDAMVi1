using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_Puntuaje : MonoBehaviour
{

    private Coroutine multiCoroutine;
    private int multiPuntos = 1;
    public void addPuntos(int puntosConseguidos)
    {
        int puntosFinales = puntosConseguidos * multiPuntos;
        HUDController.instance.AddScore(puntosFinales);
        GameManager.instance.AddScore(puntosFinales);
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
