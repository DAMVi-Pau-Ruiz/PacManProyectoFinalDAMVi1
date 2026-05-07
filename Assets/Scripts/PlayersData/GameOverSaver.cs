using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string nombre = PlayerPrefs.GetString("username", "AAA");
        int puntuacion = GameManager.instance.getScoreActual();
        GameManager.instance.saver.GuardarDatos(nombre, puntuacion);

        Debug.Log("Datos guardados al entrar en GameOver");
    }
}
